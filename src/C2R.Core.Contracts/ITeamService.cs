namespace C2R.Core.Contracts
{
    public interface ITeamService
    {
        void CreateTeam(Team team);

        void GetTeam(long telegramChatId);
        
        void AddTeamMember(long teamId, TeamMember teamMember);

        void RemoveTeamMember(long teamId, long teamMemberId);
    }
}