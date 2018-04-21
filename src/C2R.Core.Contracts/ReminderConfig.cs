using System;

namespace C2R.Core.Contracts
{
    public class ReminderConfig
    {
        public long Id { get; set; }
        
        public long TeamId { get; set; }
        
        public Guid CodeReviewerProvidingStratagy { get; set; }
        
        public TimeSpan RemindTimeUtc { get; set; }
    }
}