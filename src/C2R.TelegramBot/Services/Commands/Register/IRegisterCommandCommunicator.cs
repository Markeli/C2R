using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communications;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands
{
    public interface IRegisterCommandCommunicator : ICommunicator
    {
        Task NotifyOnAlreadyRegisteredUserAssync(ChatId chatId, TeamMember teamMember);
        
        Task NotifyOnSuccessAssync(ChatId chatId, TeamMember teamMember);

        Task NotifyOnFailureAsync(ChatId chatId);
    }
}