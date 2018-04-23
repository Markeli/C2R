using System.Threading.Tasks;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ITeamService
    {
        Task<long> CreateTeamAsync([NotNull] Team team);

        Task DeleteTeamAsync(long teamId);

        Task<bool> IsTeamRegisteredAsync(long telegramChatId);
        
        [NotNull]
        Task<Team> GetTeamAsync(long telegramChatId);
        
        Task AddTeamMemberAsync(long teamId, [NotNull] TeamMember teamMember);

        Task RemoveTeamMemberAsync(long teamId, long teamMemberId);
    }
}