﻿using System;
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

namespace C2R.TelegramBot.Services.Commands.Stop
{
    [Obsolete("Will delete later")]
    public class StopCommandProcessor : IUpdateProcessor
    {
        private readonly string _commandName = "/stop";
        [NotNull] 
        private readonly ILogger _logger;

        [NotNull] 
        private readonly ITeamService _teamService;
        
        [NotNull] 
        private readonly ITeamConfigService _configService;
        
        [NotNull]
        private readonly IReminderScheduler _reminderScheduler;

        [NotNull]
        private readonly ICommunicatorFactory _communicatorsFactory;

        public bool IsStartProcessor => false;
        
        public StopCommandProcessor(
            [NotNull] ILogger<StopCommandProcessor> logger, 
            [NotNull] ITeamService teamService, 
            [NotNull] ITeamConfigService configService, 
            [NotNull] IReminderScheduler reminderScheduler, 
            [NotNull] ICommunicatorFactory responseBuilderFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _reminderScheduler = reminderScheduler ?? throw new ArgumentNullException(nameof(reminderScheduler));
            _communicatorsFactory = responseBuilderFactory ?? throw new ArgumentNullException(nameof(responseBuilderFactory));
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
            var config = await _configService
                .GetConfigAsync(team.Id)
                .ConfigureAwait(false);

            var communicator =
                _communicatorsFactory.Create<IStopCommandCommunicator>(config.CommunicationMode);

            try
            {
                _teamService.DeleteTeamAsync(team.Id);
                _configService.DeleteConfigAsync(config.Id);
                
                if (config.ReminderJobId.HasValue)
                {
                    _reminderScheduler.TryDeleteReminder(config.ReminderJobId.Value);
                }

                await communicator.NotifyOnSuccessAssync(chatId).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error on stop command processing: {e.Message}", e);
                await communicator.NotifyOnFailureAsync(chatId).ConfigureAwait(false);
            }
        }
    }
}