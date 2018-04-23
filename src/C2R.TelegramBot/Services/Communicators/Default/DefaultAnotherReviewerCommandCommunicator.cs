using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Commands.AnotherReviewer;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Communicators.Default
{
    public class DefaultAnotherReviewerCommandCommunicator : IAnotherReviewerCommandCommunicator
    {
        [NotNull]
        private readonly IBotService _botService;

        public DefaultAnotherReviewerCommandCommunicator([NotNull] IBotService botService)
        {
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
        }

        public Task NotifyOnNoReviewerAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Увы, некому проводить ревью, никто об этом меня не просил. Пусть кто-нибудь сначала зарегистрируется");
        }

        public Task NotifyOnSuccessAsync(ChatId chatId, CodeReviewerResponse codeReviewerResponse)
        {
            if (codeReviewerResponse.CodeReviwer == null) throw new ArgumentNullException(nameof(codeReviewerResponse.CodeReviwer));
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"Ладно. Тогда код ревью проводит @{codeReviewerResponse.CodeReviwer.TelegramUsername}");
        }

        public Task NotifyOnFailureAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Произошла внутрення ошибка при выборе другого ревьюера. Мой создалеть уже в курсе");
        }
    }
}