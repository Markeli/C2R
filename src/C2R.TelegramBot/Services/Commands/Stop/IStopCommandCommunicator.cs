using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Stop
{
    public interface IStopCommandCommunicator
    {
        Task NotifyOnSuccessAssync(ChatId chatId);

        Task NotifyOnFailureAsync(ChatId chatId);
    }
}