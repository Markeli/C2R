using JetBrains.Annotations;
using Telegram.Bot;

namespace C2R.TelegramBot.Services.Bots
{
    public interface IBotService
    {
        [NotNull]
        TelegramBotClient Client { get; }
    }
}