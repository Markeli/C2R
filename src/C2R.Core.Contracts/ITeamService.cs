using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ITeamService
    {
        long CreateTeam([NotNull] Team team);

        void DeleteTeam(long teamId);

        bool IsTeamRegistered(long telegramChatId);
        
        [NotNull]
        Team GetTeam(long telegramChatId);
        
        void AddTeamMember(long teamId, [NotNull] TeamMember teamMember);

        void RemoveTeamMember(long teamId, long teamMemberId);
    }
}