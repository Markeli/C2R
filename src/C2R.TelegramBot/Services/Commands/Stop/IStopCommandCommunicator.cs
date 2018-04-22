using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communications;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Stop
{
    public interface IStopCommandCommunicator : ICommunicator
    {
        Task NotifyOnSuccessAssync(ChatId chatId);

        Task NotifyOnFailureAsync(ChatId chatId);
    }
}