using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communicators;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Register
{
    public interface IRegisterCommandCommunicator : ICommunicator
    {
        Task NotifyOnAlreadyRegisteredUserAssync(ChatId chatId, TeamMember teamMember);
        
        Task NotifyOnSuccessAssync(ChatId chatId, TeamMember teamMember);

        Task NotifyOnFailureAsync(ChatId chatId);
    }
}