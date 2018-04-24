using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communicators;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Register
{
    public interface IRegisterCommandCommunicator : ICommunicator
    {
        [NotNull]
        Task NotifyOnAlreadyRegisteredUserAssync(ChatId chatId, TeamMember teamMember);
        
        [NotNull]
        Task NotifyOnSuccessAssync(ChatId chatId, TeamMember teamMember);

        [NotNull]
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}