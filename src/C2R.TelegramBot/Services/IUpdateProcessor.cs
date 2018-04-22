using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2RTelegramBot.Services.Commands
{
    public interface IUpdateProcessor
    {
        bool CanProcess([NotNull] Update update);
        
        Task ProcessAsync([NotNull] Update update);
    }
}