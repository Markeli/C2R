using System;

namespace C2R.Core.Contracts
{
    public class TeamConfig
    {
        public long Id { get; set; }
        
        public long TeamId { get; set; }
        
        public long? ReminderJobId { get; set; }
        
        public Guid CodeReviewerProvidingStrategyId { get; set; }
        
        public Guid CommunicationMode { get; set; }
        
        public TimeSpan RemindTimeUtc { get; set; }
    }
}