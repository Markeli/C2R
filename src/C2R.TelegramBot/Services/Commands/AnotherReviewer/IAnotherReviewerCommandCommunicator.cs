﻿using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communicators;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.AnotherReviewer
{
    public interface IAnotherReviewerCommandCommunicator : ICommunicator
    {
        [NotNull]
        Task NotifyOnNoReviewerAsync(
            [NotNull] ChatId chatId);
        
        [NotNull]
        Task NotifyOnSuccessAsync(
            [NotNull] ChatId chatId, 
            [NotNull] CodeReviewerResponse codeReviewerResponse);
        
        [NotNull]
        Task NotifyOnFailureAsync(ChatId chatId);
        
        [NotNull]
        Task NotifyOnNoTeamAsync(ChatId chatId);
    }
}