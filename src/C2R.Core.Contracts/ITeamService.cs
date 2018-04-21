using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ITeamService
    {
        void CreateTeam([NotNull] Team team);

        [NotNull]
        Team GetTeam(long telegramChatId);
        
        void AddTeamMember(long teamId, [NotNull] TeamMember teamMember);

        void RemoveTeamMember(long teamId, long teamMemberId);
    }
}