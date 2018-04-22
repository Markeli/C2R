﻿using System.Threading.Tasks;
using C2R.TelegramBot.Services.Communications;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands
{
    public interface IStartCommandCommunicator : ICommunicator
    {
        Task NotifyOnAlreadyStartedAssync(ChatId chatId);
        
        Task NotifyOnSuccessAssync(ChatId chatId);

        Task NotifyOnFailureAsync(ChatId chatId);
    }
}