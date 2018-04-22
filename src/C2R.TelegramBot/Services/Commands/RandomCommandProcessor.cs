using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Extensions;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Scheduler;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace C2R.TelegramBot.Services.Commands
{
    public class RandomCommandProcessor : IUpdateProcessor
    {   
        private readonly string _commandName = "/random";

        [NotNull]
        private readonly ILogger _logger;
        [NotNull]
        private readonly IBotService _botService;

        [NotNull]
        private readonly ITeamService _teamService;

        [NotNull]
        private readonly IReminderConfigService _configService;


        [NotNull]
        private readonly IRandomCodeReviewerProviderStrategy _codeReviewerProvider;
        
        public RandomCommandProcessor(
            [NotNull] ILogger logger, 
            [NotNull] IBotService botService, 
            [NotNull] ITeamService teamService, 
            [NotNull] IReminderConfigService configService, 
            [NotNull] IRandomCodeReviewerProviderStrategy codeReviewerProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _codeReviewerProvider = codeReviewerProvider;
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
            
            var team = _teamService.GetTeam(update.GetChatId().Identifier);


            var reviewer = _codeReviewerProvider.GetCodeReviewer(ignoreHistory: true, team: team);
            
            await _botService.Client.SendTextMessageAsync(update.GetChatId(), update.Message.Text);
        }
    }
}