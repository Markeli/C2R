using System;
using System.Threading.Tasks;
using C2RTelegramBot.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace C2RTelegramBot.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        [NotNull]
        private readonly IUpdateService _updateService;

        public UpdateController([NotNull] IUpdateService updateService)
        {
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            await _updateService.ProcessUpdateAsync(update).ConfigureAwait(false);
            return Ok();
        }
    }
}