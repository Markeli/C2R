using System;
using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communicators;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Stop
{
    [Obsolete("Will be deleted later")]
    public interface IStopCommandCommunicator : ICommunicator
    {
        [NotNull]
        Task NotifyOnSuccessAssync(ChatId chatId);

        [NotNull]
        Task NotifyOnFailureAsync(ChatId chatId);
    }
}