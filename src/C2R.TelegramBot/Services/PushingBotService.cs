using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace C2RTelegramBot.Services
{
    public class PushingBotService : IBotService
    {
        private readonly IUpdateService _updateService;

        [NotNull]
        private readonly BotConfiguration _config;

        public PushingBotService([NotNull] IOptions<BotConfiguration> config, IUpdateService updateService)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _updateService = updateService;
            _config = config.Value;
            Client = new TelegramBotClient(_config.BotToken);
            Client.OnUpdate += ClientOnOnUpdate;
            Client.StartReceiving();
        }

        private void ClientOnOnUpdate(object sender, UpdateEventArgs e)
        {
            _updateService.ProcessUpdateAsync(e.Update);
        }

        public TelegramBotClient Client { get; }
    }
}