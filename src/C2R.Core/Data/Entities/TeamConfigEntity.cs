using System;

namespace C2R.Core.Data.Entities
{
    public class TeamConfigEntity
    {
        public long Id { get; set; }
        
        
        public long? ReminderJobId { get; set; }
        
        public Guid CodeReviewerProvidingStrategy { get; set; }
        
        public Guid CommunicationMode { get; set; }
        
        public TimeSpan RemindTimeUtc { get; set; }
        
        public long TeamId { get; set; }
        
        public virtual TeamEntity Team { get; set; }
    }
}