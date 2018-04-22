using Telegram.Bot;

namespace C2RTelegramBot.Services
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}