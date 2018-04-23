using System;
using System.Threading.Tasks;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Commands.StartReminder;
using JetBrains.Annotations;
using Telegram.Bot.Types;

namespace C2R.TelegramBot.Services.Communicators.Default
{
    public class DefaultStartReminderCommandCommunicator : IStartReminderCommandCommunicator
    {
        [NotNull]
        private readonly IBotService _botService;

        public DefaultStartReminderCommandCommunicator([NotNull] IBotService botService)
        {
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
        }

        public Task NotifyOnAlreadyStartedAsync(ChatId chatId, TimeSpan remindTimeUtc)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"Я уже напоминаю о код ревью каждый день в {remindTimeUtc} (UTC)");
        }

        public Task NotifyOnSuccessAsync(ChatId chatId, TimeSpan remindTimeUtc)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    $"Потрясно. Я буду напоминать о код ревью каждый день в {remindTimeUtc} (UTC)");
        }

        public Task NotifyOnFailureAsync(ChatId chatId)
        {
            return _botService.Client
                .SendTextMessageAsync(
                    chatId,
                    "Мой память меня подводит. Я не смогу вспомнить, когда вам напомнить о код ревью. Пошел за таблетками к врачу");
        }
    }
}