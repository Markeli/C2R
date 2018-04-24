using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Commands.Random;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Communicators.Default
{
    public class StandartRandomCommandCommunicator : IRandomCommandCommunciator
    {
        [NotNull]
        private readonly IBotService _botService;

        public StandartRandomCommandCommunicator([NotNull] IBotService botService)
        {
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
        }

        public Task NotifyOnNoReviewerAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Никто не зарегистрировался для проведения код ревью");
        }

        public Task NotifyOnSuccessAsync(ChatId chatId, CodeReviewerResponse codeReviewerResponse)
        {
            if (codeReviewerResponse.CodeReviwer == null) throw new ArgumentNullException(nameof(codeReviewerResponse.CodeReviwer));
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"Я сделал свой выбор. Код ревью проводит @{codeReviewerResponse.CodeReviwer.TelegramUsername}");
        }

        public Task NotifyOnReselectedrReviewerAsync(ChatId chatId, CodeReviewerResponse codeReviewerResponse)
        {
            if (codeReviewerResponse.CodeReviwer == null) throw new ArgumentNullException(nameof(codeReviewerResponse.CodeReviwer));
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"Жребий уже определял, кто проводит сегодня код ревью. Это @{codeReviewerResponse.CodeReviwer.TelegramUsername}!");
        }

        public Task NotifyOnFailureAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Произошла внутрення ошибка при выборе случайного ревьюера. Мой создать уже в курсе. Он меня вылечит");
        }
    }
}