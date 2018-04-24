using C2R.Core.Contracts;
using C2R.Core.Data.Entities;

namespace C2R.Core.Mappers
{
    public static class TeamConfigMapper
    {
        public static TeamConfig ToDomain(this TeamConfigEntity entity)
        {
            if (entity == null) return null;
            
            return new TeamConfig
            {
                Id = entity.Id,
                TeamId = entity.TeamId,
                ReminderJobId = entity.ReminderJobId,
                RemindTimeUtc = entity.RemindTimeUtc,
                CodeReviewerProvidingStrategyId = entity.CodeReviewerProvidingStrategy,
                CommunicationMode = entity.CommunicationMode
            };
        }
        
        public static TeamConfigEntity ToEntity(this TeamConfig  config)
        {
            if (config == null) return null;
            
            return new TeamConfigEntity
            {
                Id = config.Id,
                TeamId = config.TeamId,
                ReminderJobId = config.ReminderJobId,
                RemindTimeUtc = config.RemindTimeUtc,
                CodeReviewerProvidingStrategy = config.CodeReviewerProvidingStrategyId,
                CommunicationMode = config.CommunicationMode
            };
        }
    }
}