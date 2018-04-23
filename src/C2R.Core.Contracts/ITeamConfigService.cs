using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ITeamConfigService
    {
        void SetDefaultProviderStrategy(Guid id);
        
        void SetDefaultCommunication(Guid id);

        [NotNull]
        TeamConfig GetDefaultConfig();
        
        Task CreateConfigAsync([NotNull] TeamConfig config);
        
        [NotNull]
        Task<TeamConfig> GetConfigAsync(long teamId);

        Task DeleteConfigAsync(long configId);

        Task UpdateRemindTimeAsync(long teamId, TimeSpan remindTime);

    }
}