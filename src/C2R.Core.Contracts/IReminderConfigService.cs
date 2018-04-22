using System;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface IReminderConfigService
    {
        void SetDefaultProviderStrategy(Guid id);
        
        void SetDefaultCommunication(Guid id);
        
        void CreateDefaultConfig(long teamId);
        
        [NotNull]
        ReminderConfig GetConfig(long teamId);

        void DeleteConfig(long configId);

        void UpdateRemindTime(long teamId, TimeSpan remindTime);

        void UpdateLastRemindDateTime(long teamId, DateTime remindTimeUtc);
    }
}