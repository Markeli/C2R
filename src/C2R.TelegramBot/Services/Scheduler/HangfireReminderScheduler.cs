using System;
using System.Threading.Tasks;
using Hangfire;

namespace C2R.TelegramBot.Services.Scheduler
{
    public class HangfireReminderScheduler : IReminderScheduler

    {
        private readonly IReminder _reminder;
        
        public bool IsReminderCreated(long teamId)
        {
            var a = new RecurringJobManager();
            a.RemoveIfExists();
        }

        public void CreateReminder(long teamId, TimeSpan remindTimeUtc)
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteReminder(long teamId)
        {
            throw new NotImplementedException();
        }
    }

    public interface IReminder
    {
        Task RemindAsync(long teamId);
    }
}