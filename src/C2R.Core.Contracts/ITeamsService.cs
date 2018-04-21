using System;

namespace C2R.Core.Contracts
{
    public interface ITeamsService
    {
        void CreateTeam(Team team);

        void GetTeam(long telegramChatId);
        
        void AddTeamMember(long teamId, long userId);

        void RemoveTeamMember(long teamId, long userId);
        
        
    }

    public interface ICodeReviewerProviderStrategy
    {
        Guid StrategyId { get; }
        
        User GetCodeReviewer(long teamId);
    }
    
    public interface IReminderConfigService
    {
        void CreateDefaultConfig(long teamId);

        ReminderConfig GetConfig(long teamId);

        void UpdateRemindTime(long teamId, TimeSpan remindTime);

        void UpdateLastRemindDateTime(long teamId, DateTime remindTimeUtc);
    }

    public class ReminderConfig
    {
        public long Id { get; set; }
        
        public long TeamId { get; set; }
        
        public Guid CodeReviewerProvidingStratagy { get; set; }
        
        public TimeSpan RemindTime { get; set; }
        
        public DateTime LastRemindDateTimeUtc { get; set; }
    }
    
    public class Team
    {
        public long Id { get; set; }
        
        public long TelegramChatId { get; set; }
    }
    
    public class User
    {
        public long Id { get; set; }
        
        public long TelegramUserId { get; set; }
    }
}