using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communicators;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.StopReminders
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