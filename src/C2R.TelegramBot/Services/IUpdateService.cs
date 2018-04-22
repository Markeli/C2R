using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2RTelegramBot.Services
{
    public interface IUpdateService
    {
        Task ProcessUpdateAsync([NotNull] Update update);
    }
}