using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communicators;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Reviewer
{
    public interface IReviewerCommandCommunicator : ICommunicator
    {
        Task NotifyOnNoReviewerAsync(
            [NotNull] ChatId chatId);
        
        Task NotifyOnSuccessAsync(
            [NotNull] ChatId chatId, 
            [NotNull] CodeReviewerResponse codeReviewerResponse);
        
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}