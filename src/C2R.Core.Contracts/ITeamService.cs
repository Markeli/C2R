using System.Threading.Tasks;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ITeamService
    {
        [NotNull]
        Task<long> CreateTeamAsync([NotNull] Team team);

        [NotNull]
        Task DeleteTeamAsync(long teamId);

        [NotNull]
        Task<bool> IsTeamRegisteredAsync(long telegramChatId);
        
        [NotNull]
        Task<Team> GetTeamAsync(long telegramChatId);
        
        [NotNull]
        Task AddTeamMemberAsync(long teamId, [NotNull] TeamMember teamMember);

        [NotNull]
        Task RemoveTeamMemberAsync(long teamId, long teamMemberId);
    }
}