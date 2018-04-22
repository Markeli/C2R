﻿using System;
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
    public class StartCommandProccessor : IUpdateProcessor
    {
        private readonly string _commandName = "/start";

        [NotNull]
        private readonly ILogger _logger;
        [NotNull]
        private readonly IBotService _botService;

        [NotNull]
        private readonly ITeamService _teamService;

        [NotNull]
        private readonly IReminderConfigService _configService;

        public StartCommandProccessor(
            [NotNull] ILogger<StartCommandProccessor> logger, 
            [NotNull] IBotService botService,
            [NotNull] ITeamService teamService,
            [NotNull] IReminderConfigService configService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
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
            if (!canProcess) throw new ArgumentException($"{GetType().Name} can not procces message update with Id {update.Id} and type {update.Type}");

            
            var team = new Team
            {
                TelegramChatId = update.GetChatId().Identifier
            };
            var teamId = _teamService.CreateTeam(team);

            _configService.CreateDefaultConfig(teamId);
            var config = _configService.GetConfig(teamId);
            
            await _botService.Client.SendTextMessageAsync(update.GetChatId(), update.Message.Text);
        }
    }
}