using System;
using System.Linq;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Extensions;
using C2R.TelegramBot.Services.Bots;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace C2R.TelegramBot.Services.Commands
{
    public class UnregisterCommandProcessor : IUpdateProcessor
    { 
        private readonly string _commandName = "/unregister";

        [NotNull] private readonly ILogger _logger;
        [NotNull] private readonly IBotService _botService;

        [NotNull] private readonly ITeamService _teamService;


        public UnregisterCommandProcessor(
            [NotNull] ILogger<RegisterCommandProcessor> logger,
            [NotNull] IBotService botService,
            [NotNull] ITeamService teamService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
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

            var team = _teamService.GetTeam(update.GetChatId().Identifier);

            var telegramUserId = update.Message.From.Id;
            var teamMember = team.Members.FirstOrDefault(x => x.TelegramUserId == telegramUserId);
            if (teamMember == null)
            {
                await _botService.Client.SendTextMessageAsync(update.GetChatId(), update.Message.Text);
                return;
            }
            _teamService.RemoveTeamMember(team.Id, teamMember.Id);

            await _botService.Client.SendTextMessageAsync(update.GetChatId(), update.Message.Text);
        }
    }
}