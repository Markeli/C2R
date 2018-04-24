using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communicators;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.StopReminders
{
    public interface IStopReminderCommandCommunicator : ICommunicator
    {
        
        [NotNull]
        Task NotifyOnNotYetStartedAsync(
            [NotNull] ChatId chatId);
        
        [NotNull]
        Task NotifyOnSuccessAsync(
            [NotNull] ChatId chatId);
        
        [NotNull]
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}