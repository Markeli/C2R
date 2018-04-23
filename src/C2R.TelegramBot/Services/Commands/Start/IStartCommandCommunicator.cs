using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communicators;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Start
{
    public interface IStartCommandCommunicator : ICommunicator
    {
        Task NotifyOnAlreadyStartedAssync(ChatId chatId);
        
        Task NotifyOnSuccessAssync(ChatId chatId);

        Task NotifyOnFailureAsync(ChatId chatId);
    }
}