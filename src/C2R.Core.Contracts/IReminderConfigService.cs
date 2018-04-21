using System;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface IReminderConfigService
    {
        void CreateDefaultConfig(long teamId);

        [NotNull]
        ReminderConfig GetConfig(long teamId);

        void UpdateRemindTime(long teamId, TimeSpan remindTime);

        void UpdateLastRemindDateTime(long teamId, DateTime remindTimeUtc);
    }
}