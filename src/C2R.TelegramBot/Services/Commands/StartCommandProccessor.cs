using System.Threading.Tasks;
using C2RTelegramBot.Services.Commands;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands
{
    public class StartCommandProccessor : IUpdateProcessor
    {
        public Task<bool> CanProcess(Update update)
        {
            throw new System.NotImplementedException();
        }

        public Task ProcessAsync(Update update)
        {
            throw new System.NotImplementedException();
        }
    }
}