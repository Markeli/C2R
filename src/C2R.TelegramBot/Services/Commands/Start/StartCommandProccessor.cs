using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Extensions;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Communications;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace C2R.TelegramBot.Services.Commands
{
    public class StartCommandProccessor : IUpdateProcessor
    {
        private readonly string _commandName = "/start";

        [NotNull]
        private readonly ILogger _logger;

        [NotNull]
        private readonly ITeamService _teamService;

        [NotNull]
        private readonly ITeamConfigService _configService;

        [NotNull]
        private readonly ICommunicatorFactory _communicatorFactory;
        
        public StartCommandProccessor(
            [NotNull] ILogger<StartCommandProccessor> logger, 
            [NotNull] ITeamService teamService,
            [NotNull] ITeamConfigService configService, 
            [NotNull] ICommunicatorFactory communicatorFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
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

            var isTeamRegisted = await _teamService
                .IsTeamRegisteredAsync(chatId.Identifier)
                .ConfigureAwait(false);
            TeamConfig config;
            if (isTeamRegisted)
            {
                var team = await _teamService
                    .GetTeamAsync(chatId.Identifier)
                    .ConfigureAwait(false);
                config = _configService.GetConfig(team.Id);
            }
            else
            {
                config = _configService.GetDefaultConfig();
                
            }
            
            var communicator = _communicatorFactory.Create<IStartCommandCommunicator>(config.CommunicationMode);
            try
            {
                if (isTeamRegisted)
                {
                    await communicator.NotifyOnAlreadyStartedAssync(chatId)
                        .ConfigureAwait(false);
                    return;
                }
                
                var team = new Team
                {
                    TelegramChatId = chatId.Identifier
                };
                var teamId = await _teamService
                    .CreateTeamAsync(team)
                    .ConfigureAwait(false);
                config.TeamId = teamId;

                _configService.CreateConfig(config);

               await communicator.NotifyOnSuccessAssync(chatId).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await communicator.NotifyOnFailureAsync(chatId).ConfigureAwait(false);
                _logger.LogError($"Error on start command: {e.Message}", e);
            }
        }
    }
}