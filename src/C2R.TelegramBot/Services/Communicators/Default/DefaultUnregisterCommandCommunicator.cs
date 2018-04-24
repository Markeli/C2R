using System;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Bots;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Commands.Unregister.Communicators
{
    public class DefaultUnregisterCommandCommunicator : IUnregisterCommandCommunicator
    {
        [NotNull]
        private readonly IBotService _botService;

        public DefaultUnregisterCommandCommunicator([NotNull] IBotService botService)
        {
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
        }

        public Task NotifyOnNotRegisteredUserAsync(ChatId chatId, TeamMember teamMember)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"@{teamMember.TelegramUsername}, ты сначала зарегистрируйся, а потом отписывайся");
        }

        public Task NotifyOnSuccessAsync(ChatId chatId, TeamMember teamMember)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"Хорошо, @{teamMember.TelegramUsername}, ты больше не участвуешь в код ревью");
        }

        public Task NotifyOnFailureAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Прости, я не смог удалить тебя из списков, уже попросил создателя разобраться");
        }
    }
}