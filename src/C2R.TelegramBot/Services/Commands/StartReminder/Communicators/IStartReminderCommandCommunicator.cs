using System;
using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communications;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands
{
    public interface IStartReminderCommandCommunicator : ICommunicator
    {
        
        Task NotifyOnAlreadyStartedAsync(
            [NotNull] ChatId chatId,
            TimeSpan remindTimeUtc);
        
        Task NotifyOnSuccessAsync(
            [NotNull] ChatId chatId,
            TimeSpan remindTimeUtc);
        
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}