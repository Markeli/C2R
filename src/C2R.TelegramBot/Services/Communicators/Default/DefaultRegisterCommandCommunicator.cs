using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Bots;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands
{
    public class DefaultRegisterCommandCommunicator : IRegisterCommandCommunicator
    {
        [NotNull]
        private readonly IBotService _botService;

        public DefaultRegisterCommandCommunicator([NotNull] IBotService botService)
        {
            _botService = botService;
        }

        public Task NotifyOnAlreadyRegisteredUserAssync(ChatId chatId, TeamMember teamMember)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"@{teamMember.TelegramUsername}, ты уже зарегистрировался для проведения код ревью");
        }

        public Task NotifyOnSuccessAssync(ChatId chatId, TeamMember teamMember)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"@{teamMember.TelegramUsername}, хорошо, я запомнил, что ты тоже хочешь проводить код ревью");
        }

        public Task NotifyOnFailureAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Аааа! Я поломался. Не смогу запомнить, что тебе надо проводить ревью. Создатель посмотрит, что пошло не так");
        }
    }
}