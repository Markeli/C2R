using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace C2R.TelegramBot.Services.Bots
{
    public class BotService : IBotService
    {
        [NotNull]
        private readonly BotConfiguration _config;

        public BotService([NotNull] IOptions<BotConfiguration> config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _config = config.Value;
            Client = new TelegramBotClient(_config.BotToken);
        }


        public TelegramBotClient Client { get; }
    }
}