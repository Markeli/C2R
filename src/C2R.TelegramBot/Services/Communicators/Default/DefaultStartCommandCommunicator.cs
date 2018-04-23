using System;
using System.Threading.Tasks;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Commands.Start;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Communicators.Default
{
    public class DefaultStartCommandCommunicator : IStartCommandCommunicator
    {
        [NotNull]
        private readonly IBotService _botService;

        public DefaultStartCommandCommunicator([NotNull] IBotService botService)
        {
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
        }

        public Task NotifyOnAlreadyStartedAssync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Ребята, мы давно знакомы, и эту команды мы с вами уже выполняли");
        }

        public Task NotifyOnSuccessAssync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Отлично! Мы стали на шаг ближе для постоянного проведения код ревью. Теперь участникам команды нужно зарегистрироваться, чтобы я мог выбирать ревьюера");
        }

        public Task NotifyOnFailureAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Все плохо. Я заболел. Простите, не смог, выполнить команду. Уже позвонил врачу");
        }
    }
}