using System;

namespace C2R.TelegramBot.Services.Scheduler
{
    public interface IReminderScheduler
    {
        long CreateReminder(long teamId, TimeSpan remindTimeUtc);

        bool TryDeleteReminder(long remindJobId);
    }
}