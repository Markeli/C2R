using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using C2R.TelegramBot.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        [NotNull]
        private readonly IUpdateService _updateService;

        [NotNull]
        private readonly BlockingCollection<Update> _updates;

        [NotNull]
        private readonly ILogger _logger;

        private const int UpdatesCollectionMaxSize = 10000;
        
        public UpdateController([NotNull] IUpdateService updateService, 
            [NotNull] ILogger<UpdateController> logger)
        {
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _logger = logger;
            _updates = new BlockingCollection<Update>(UpdatesCollectionMaxSize);

            Task.Factory.StartNew(async () => ProcessUpdatesAsync(), TaskCreationOptions.LongRunning);
        }

        private async Task ProcessUpdatesAsync()
        {
            foreach (var update in _updates.GetConsumingEnumerable())
            {
                try
                {
                    await _updateService
                        .ProcessUpdateAsync(update)
                        .ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error on update processig: {e.Message}", e);
                }
            }
        }

        // POST api/update
        [HttpPost]
        public IActionResult Post([FromBody]Update update)
        {
            if (update == null) return BadRequest();

            return _updates.TryAdd(update)
                ? Ok()
                : StatusCode(500);
        }
    }
}