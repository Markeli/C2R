using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Bots;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands
{
    public class DefaultReviewerCommandCommunicator : IReviewerCommandCommunicator
    {
        [NotNull]
        private readonly IBotService _botService;

        public DefaultReviewerCommandCommunicator([NotNull] IBotService botService)
        {
            _botService = botService;
        }

        public Task NotifyOnNoReviewerAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Я не могу никого выбрать для проведения код ревью, ведь надо сначала зарегистрироваться");
        }

        public Task NotifyOnSuccessAsync(ChatId chatId, CodeReviewerResponse codeReviewerResponse)
        {
            if (codeReviewerResponse.CodeReviwer == null) throw new ArgumentNullException(nameof(codeReviewerResponse.CodeReviwer));
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"И сегодня код ревью проводит @{codeReviewerResponse.CodeReviwer.TelegramUsername}");
        }

        public Task NotifyOnFailureAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Что-то пошло не так, и я не смог выбрать ревьюера. Мой создатель разберется с этой проблемой");
        }
    }
}