using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Extensions;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Communications;
using C2R.TelegramBot.Services.Scheduler;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace C2R.TelegramBot.Services.Commands
{
    public class StartReminderCommandProcessor : IUpdateProcessor
    {
        private readonly string _commandName = "/start_reminders";

        [NotNull]
        private readonly ILogger _logger;
        [NotNull]
        private readonly IBotService _botService;

        [NotNull]
        private readonly ITeamService _teamService;

        [NotNull]
        private readonly ITeamConfigService _configService;

        [NotNull] private readonly IReminderScheduler _reminderScheduler;

        [NotNull]
        private readonly ICommunicatorFactory _communicatorFactory;

        public StartReminderCommandProcessor(
            [NotNull] ILogger logger, 
            [NotNull] IBotService botService, 
            [NotNull] ITeamService teamService, 
            [NotNull] ITeamConfigService configService, 
            [NotNull] IReminderScheduler reminderScheduler, 
            [NotNull] ICommunicatorFactory communicatorFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _reminderScheduler = reminderScheduler ?? throw new ArgumentNullException(nameof(reminderScheduler));
            _communicatorFactory = communicatorFactory ?? throw new ArgumentNullException(nameof(communicatorFactory));
        }

        public bool CanProcess(Update update)
        { 
            if (update == null) throw new ArgumentNullException(nameof(update));
            if (update.Type != UpdateType.MessageUpdate) return false;

            if (update.Message == null)
            {
                _logger.LogWarning($"Received empty message in update with ID {update.Id}");
                return false;
            }

            var canProcess = !String.IsNullOrEmpty(update.Message.Text) && update.Message.Text.StartsWith(_commandName);
            return canProcess;
        }

        public async Task ProcessAsync(Update update)
        {
            var canProcess = CanProcess(update);
            if (!canProcess) throw new ArgumentException($"{GetType().Name} can not procces message update with Id {update.Id} and type {update.Type}");

            var chatId = update.GetChatId();
            var team = _teamService.GetTeamAsync(chatId.Identifier);
            var config = _configService.GetConfig(team.Id);

            var communicator = _communicatorFactory.Create<IStartReminderCommandCommunicator>(config.CommunicationMode);
            
            try
            {
                if (_reminderScheduler.IsReminderCreated(team.Id))
                {
                    await communicator.NotifyOnAlreadyStartedAsync(chatId, config.RemindTimeUtc)
                        .ConfigureAwait(false);
                    return;
                }
                _reminderScheduler.CreateReminder(team.Id, config.RemindTimeUtc);
                communicator.NotifyOnSuccessAsync(chatId, config.RemindTimeUtc)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await communicator.NotifyOnFailureAsync(chatId).ConfigureAwait(false);
                _logger.LogError($"Error on start reminder command: {e.Message}", e);
            }
            
        }
    }
}