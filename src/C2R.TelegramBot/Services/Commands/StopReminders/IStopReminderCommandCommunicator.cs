using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communications;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands
{
    public interface IStopReminderCommandCommunicator : ICommunicator
    {
        
        Task NotifyOnNotYetStartedAsync(
            [NotNull] ChatId chatId);
        
        Task NotifyOnSuccessAsync(
            [NotNull] ChatId chatId);
        
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}