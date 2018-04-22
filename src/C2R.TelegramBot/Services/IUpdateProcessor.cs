using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace C2RTelegramBot.Services.Commands
{
    public interface IUpdateProcessor
    {
        Task<bool> CanProcess(Update update);
        
        Task ProcessAsync(Update update);
    }
}