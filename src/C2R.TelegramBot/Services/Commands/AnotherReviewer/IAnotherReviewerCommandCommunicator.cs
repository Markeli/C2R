using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communications;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands
{
    public interface IAnotherReviewerCommandCommunicator : ICommunicator
    {
        Task NotifyOnNoReviewerAsync(
            [NotNull] ChatId chatId);
        
        Task NotifyOnSuccessAsync(
            [NotNull] ChatId chatId, 
            [NotNull] CodeReviewerResponse codeReviewerResponse);
        
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}