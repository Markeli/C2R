using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communicators;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Start
{
    public interface IStartCommandCommunicator : ICommunicator
    {
        [NotNull]
        Task NotifyOnAlreadyStartedAssync(ChatId chatId);
        
        [NotNull]
        Task NotifyOnSuccessAssync(ChatId chatId);

        [NotNull]
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}