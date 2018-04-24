using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communicators;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Unregister.Communicators
{
    public interface IUnregisterCommandCommunicator : ICommunicator
    {
        [NotNull]
        Task NotifyOnNotRegisteredUserAsync(ChatId chatId, TeamMember teamMember);
        
        [NotNull]
        Task NotifyOnSuccessAsync(ChatId chatId, TeamMember teamMember);

        [NotNull]
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}