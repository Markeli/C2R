using System;

namespace C2R.Core.Contracts
{
    public interface IReminderConfigService
    {
        void CreateDefaultConfig(long teamId);

        ReminderConfig GetConfig(long teamId);

        void UpdateRemindTime(long teamId, TimeSpan remindTime);

        void UpdateLastRemindDateTime(long teamId, DateTime remindTimeUtc);
    }
}