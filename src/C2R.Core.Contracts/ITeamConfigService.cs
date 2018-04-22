using System;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ITeamConfigService
    {
        void SetDefaultProviderStrategy(Guid id);
        
        void SetDefaultCommunication(Guid id);

        [NotNull]
        TeamConfig GetDefaultConfig();
        
        void CreateConfig([NotNull] TeamConfig config);
        
        [NotNull]
        TeamConfig GetConfig(long teamId);

        void DeleteConfig(long configId);

        void UpdateRemindTime(long teamId, TimeSpan remindTime);

        void UpdateLastRemindDateTime(long teamId, DateTime remindTimeUtc);
    }
}