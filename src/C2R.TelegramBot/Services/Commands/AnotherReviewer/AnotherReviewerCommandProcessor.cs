using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Extensions;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Communicators;
using C2R.TelegramBot.Services.Communicators.Default;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace C2R.TelegramBot.Services.Commands.AnotherReviewer
{
    public class AnotherReviewerCommandProcessor : IUpdateProcessor
    { 
        private readonly string _commandName = "/another_reviewer";

        [NotNull]
        private readonly ILogger _logger;
        [NotNull]
        private readonly ITeamService _teamService;

        [NotNull]
        private readonly ITeamConfigService _configService;


        [NotNull]
        private readonly ICodeReviewerProvider _codeReviewerProvider;

        [NotNull]
        private readonly ICommunicatorFactory _communicatorFactory;
        
        public AnotherReviewerCommandProcessor(
            [NotNull] ILogger<AnotherReviewerCommandProcessor> logger, 
            [NotNull] ITeamService teamService, 
            [NotNull] ITeamConfigService configService, 
            [NotNull] ICodeReviewerProvider codeReviewerProvider, 
            [NotNull] ICommunicatorFactory communicatorFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _codeReviewerProvider = codeReviewerProvider;
            _communicatorFactory = communicatorFactory ?? throw new ArgumentNullException(nameof(communicatorFactory));
        }

        public bool IsStartProcessor => false;

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
            var config = await _configService
                .GetConfigAsync(team.Id)
                .ConfigureAwait(false);

            var communicator =
                _communicatorFactory.Create<IAnotherReviewerCommandCommunicator>(config.CommunicationMode);
            try
            {
                var reviewerResponse = await _codeReviewerProvider.GetCodeReviewerAsync(
                    returnTodaySelectedReviewer: false, 
                    team: team, 
                    reviewerProviderStrategyId: config.CodeReviewerProvidingStrategyId);
                if (reviewerResponse.CodeReviwer == null)
                {
                    communicator.NotifyOnNoReviewerAsync(chatId)
                        .ConfigureAwait(false);
                    return;
                }
                await communicator.NotifyOnSuccessAsync(chatId, reviewerResponse)
                    .ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await communicator.NotifyOnFailureAsync(chatId)
                    .ConfigureAwait(false);
                _logger.LogError($"Error on another reviewer command: {e.Message}", e);
            }
        }
    }
}