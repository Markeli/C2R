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
    public class ReviewerCommandProcessor : IUpdateProcessor
    {
         private readonly string _commandName = "/reviewer";

        [NotNull]
        private readonly ILogger _logger;
        [NotNull]
        private readonly IBotService _botService;

        [NotNull]
        private readonly ITeamService _teamService;

        [NotNull]
        private readonly ITeamConfigService _configService;

        [NotNull]
        private readonly ICodeReviewerProvider _codeReviewerProvider;

        [NotNull]
        private readonly ICommunicatorFactory _communicatorFactory;
        
        
        public ReviewerCommandProcessor(
            [NotNull] ILogger logger, 
            [NotNull] IBotService botService, 
            [NotNull] ITeamService teamService, 
            [NotNull] ITeamConfigService configService, 
            [NotNull] ICodeReviewerProvider codeReviewerProvider, 
            [NotNull] ICommunicatorFactory communicatorFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _codeReviewerProvider = codeReviewerProvider;
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
            var team = await _teamService
                .GetTeamAsync(chatId.Identifier)
                .ConfigureAwait(false);
            var config = _configService.GetConfig(team.Id);

            var communicator = _communicatorFactory.Create<IReviewerCommandCommunicator>(config.CommunicationMode);
            try
            {
                var reviewerResponse = _codeReviewerProvider.GetCodeReviewer(
                    ignoreHistory: false, 
                    team: team, 
                    config: config);
                if (reviewerResponse.CodeReviwer == null)
                {
                    communicator.NotifyOnNoReviewerAsync(chatId)
                        .ConfigureAwait(false);
                    return;
                }

                communicator.NotifyOnSuccessAsync(chatId, reviewerResponse)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await communicator.NotifyOnFailureAsync(chatId)
                    .ConfigureAwait(false);
                _logger.LogError($"Error on reviewer command: {e.Message}", e);
            }
            
            await _botService.Client.SendTextMessageAsync(update.GetChatId(), update.Message.Text);
        }
    }
}