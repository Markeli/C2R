using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace C2R.TelegramBot.Services.Scheduler
{
    public class ReminderScheduler : IReminderScheduler
    {
        [NotNull]
        private readonly Dictionary<long, TimeSpan> _reminders;
        
        public ReminderScheduler()
        {
            _reminders = new Dictionary<long,TimeSpan>();
        }

        public bool IsReminderCreated(long teamId)
        {
            return _reminders.ContainsKey(teamId);
        }

        public void CreateReminder(long teamId, TimeSpan remindTimeUtc)
        {
            _reminders[teamId] = remindTimeUtc;
        }

        public bool TryDeleteReminder(long teamId)
        {
            if (!_reminders.ContainsKey(teamId)) return false;

            return _reminders.Remove(teamId);
        }
    }
}