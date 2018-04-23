using System;

namespace C2R.Core.Data.Entities
{
    public class HistoryEntryEntity
    {
        public long Id { get; set; }
        
        public long ReviewedTeamMemberId { get; set; }
        
        public virtual TeamMemberEntity ReviewedTeamMember { get; set; }
        
        public DateTime ReviewDateTimeUtc { get; set; }
        
        public long TeamId { get; set; }
        
        public virtual TeamEntity Team { get; set; }
    }
}