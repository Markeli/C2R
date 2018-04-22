using JetBrains.Annotations;
using Telegram.Bot;

namespace C2RTelegramBot.Services
{
    public interface IBotService
    {
        [NotNull]
        TelegramBotClient Client { get; }
    }
}