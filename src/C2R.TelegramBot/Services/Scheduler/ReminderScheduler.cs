using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace C2R.TelegramBot.Services.Scheduler
{
    public class ReminderScheduler : IReminderScheduler
    {
        [NotNull]
        private readonly Dictionary<long, Tuple<long, TimeSpan>> _reminders;
        [NotNull]
        private readonly Random _randomizer;
        
        public ReminderScheduler()
        {
            _reminders = new Dictionary<long, Tuple<long, TimeSpan>>();
            _randomizer = new Random();
        }

        public bool IsReminderCreated(long teamId)
        {
            return _reminders.ContainsKey(teamId);
        }

        public long CreateReminder(long teamId, TimeSpan remindTimeUtc)
        {
            var id = _randomizer.Next();
            
            _reminders[teamId] = new Tuple<long, TimeSpan>(id, remindTimeUtc);

            return id;
        }

        public bool TryDeleteReminder(long remindJobId)
        {
            var remidnerInfo = _reminders.Values.FirstOrDefault(x => x.Item1 == remindJobId);
            if (remidnerInfo == null) return false;

            var teamId = _reminders.First(x => Equals(x.Value, remidnerInfo));

            return _reminders.Remove(teamId.Key);
        }
    }
}