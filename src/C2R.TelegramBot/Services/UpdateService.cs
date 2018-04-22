using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C2RTelegramBot.Services.Commands;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace C2RTelegramBot.Services
{
    public class UpdateService : IUpdateService
    {
        [NotNull] 
        private readonly ICollection<IUpdateProcessor> _updateProcessors;

        [NotNull] private readonly ILogger<UpdateService> _logger;

        public UpdateService(
            [NotNull] ICollection<IUpdateProcessor> updateProcessors,
            [NotNull] ILogger<UpdateService> logger)
        {
            _updateProcessors = updateProcessors ?? throw new ArgumentNullException(nameof(updateProcessors));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ProcessUpdateAsync(Update update)
        {
            if (update == null) throw new ArgumentNullException(nameof(update));

            try
            {
                var processor = _updateProcessors.FirstOrDefault(x => x.CanProcess(update));
                if (processor == null) return;

                await processor.ProcessAsync(update).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError("Processing message update error", e);
            }
        }
    }
}