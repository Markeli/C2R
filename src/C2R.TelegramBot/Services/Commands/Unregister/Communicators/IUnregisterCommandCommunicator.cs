using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communicators;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Unregister.Communicators
{
    public interface IUnregisterCommandCommunicator : ICommunicator
    {
        Task NotifyOnNotRegisteredUserAsync(ChatId chatId, TeamMember teamMember);
        
        Task NotifyOnSuccessAsync(ChatId chatId, TeamMember teamMember);

        Task NotifyOnFailureAsync(ChatId chatId);
    }
}