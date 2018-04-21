using System;

namespace C2R.Core.Contracts
{
    public class HistoryEntry
    {
        public long Id { get; set; }
        
        public long TeamId { get; set; }
        
        public long ReviewedTeamMemberId { get; set; }
        
        public DateTime ReviewDateTimeUtc { get; set; }
    }
}