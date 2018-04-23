using System;
using System.Threading.Tasks;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Commands.StopReminders;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Communicators.Default
{
    public class DefaultStopReminderCommandCommunicator : IStopReminderCommandCommunicator
    {
        [NotNull]
        private readonly IBotService _botService;

        public DefaultStopReminderCommandCommunicator([NotNull] IBotService botService)
        {
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
        }


        public Task NotifyOnNotYetStartedAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Чтобы я перстал напоминать о код ревью, надо сначала попросить меня напоминать о нем.");
        }

        public Task NotifyOnSuccessAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Как скажите. Больше не буду напоминать вам о код ревью");
        }

        public Task NotifyOnFailureAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Моя память снова меня подводит. Но теперь я могу ничего забыть и все равно буду напоминать. Пошел за таблетками к врачу");
        }
    }
}