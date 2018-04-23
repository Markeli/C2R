﻿using System;
using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communicators;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Stop
{
    [Obsolete("Will be deleted later")]
    public interface IStopCommandCommunicator : ICommunicator
    {
        Task NotifyOnSuccessAssync(ChatId chatId);

        Task NotifyOnFailureAsync(ChatId chatId);
    }
}