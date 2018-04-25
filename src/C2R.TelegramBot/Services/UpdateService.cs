using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Extensions;
using C2R.TelegramBot.Services.Bots;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services
{
    public class UpdateService : IUpdateService
    {
        [NotNull] 
        private readonly ICollection<IUpdateProcessor> _updateProcessors;

        [NotNull]
        private readonly ILogger<UpdateService> _logger;

        [NotNull]
        private readonly IBotService _botService;

        [NotNull]
        private readonly ITeamService _teamService;
        
        public UpdateService(
            [NotNull] ICollection<IUpdateProcessor> updateProcessors,
            [NotNull] ILogger<UpdateService> logger, 
            [NotNull] IBotService botService, 
            [NotNull] ITeamService teamService)
        {
            _updateProcessors = updateProcessors ?? throw new ArgumentNullException(nameof(updateProcessors));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
        }

        public async Task ProcessUpdateAsync(Update update)
        {
            if (update == null) throw new ArgumentNullException(nameof(update));

            try
            {
                var processor = _updateProcessors.FirstOrDefault(x => x.CanProcess(update));
                if (processor == null) return;

                var chatId = update.GetChatId();
                var isTeamExist = await _teamService.IsTeamRegisteredAsync(chatId.Identifier);
                if (!isTeamExist && !processor.IsStartProcessor)
                {
                    await _botService.Client.SendTextMessageAsync(update.GetChatId(), "Please, send me command `/start` to start conversation");
                    return;
                }

                await processor.ProcessAsync(update).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError($"Processing message update error: {e.Message}", e);
               await _botService.Client.SendTextMessageAsync(update.GetChatId(), "Something went wrong. We are working on it. ");
            }
        }
    }
}