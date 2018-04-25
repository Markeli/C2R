using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Extensions;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Communicators;
using C2R.TelegramBot.Services.Scheduler;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace C2R.TelegramBot.Services.Commands.StopReminders
{
    public class StopReminderCommandProcessor : IUpdateProcessor
    {
        private readonly string _commandName = "/stop_reminders";

        [NotNull] private readonly ILogger _logger;

        [NotNull] private readonly ITeamService _teamService;


        [NotNull] private readonly IReminderScheduler _reminderScheduler;

        [NotNull]
        private readonly ICommunicatorFactory _communicatorFactory;

        [NotNull]
        private readonly ITeamConfigService _configService;

        public bool IsStartProcessor => false;
        
        public StopReminderCommandProcessor(
            [NotNull] ILogger<StopReminderCommandProcessor> logger,
            [NotNull] ITeamService teamService,
            [NotNull] IReminderScheduler reminderScheduler, 
            [NotNull] ICommunicatorFactory communicatorFactory, 
            [NotNull] ITeamConfigService configService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _reminderScheduler = reminderScheduler ?? throw new ArgumentNullException(nameof(reminderScheduler));
            _communicatorFactory = communicatorFactory ?? throw new ArgumentNullException(nameof(communicatorFactory));
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
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
            if (!canProcess)
                throw new ArgumentException(
                    $"{GetType().Name} can not procces message update with Id {update.Id} and type {update.Type}");

            var chatId = update.GetChatId();
            var team = await _teamService
                .GetTeamAsync(chatId.Identifier)
                .ConfigureAwait(false);
            var config = await _configService
                .GetConfigAsync(team.Id)
                .ConfigureAwait(false);

            var communicator = _communicatorFactory.Create<IStopReminderCommandCommunicator>(config.CommunicationMode);
            try
            {
                if (!_reminderScheduler.IsReminderCreated(team.Id))
                {
                    await communicator.NotifyOnNotYetStartedAsync(chatId)
                        .ConfigureAwait(false);
                    return;
                }

                _reminderScheduler.TryDeleteReminder(team.Id);

                await communicator.NotifyOnSuccessAsync(chatId)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await communicator.NotifyOnFailureAsync(chatId).ConfigureAwait(false);
                _logger.LogError($"Error on stop reminder command: {e.Message}", e);
            }
        }

    }
}