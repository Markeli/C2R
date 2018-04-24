using System;
using System.Collections.Generic;

namespace C2R.Core.Data.Entities
{
    public class TeamMemberEntity
    {
        public long Id { get; set; }
        
        public long TelegramUserId { get; set; }
        
        public string TelegramUsername { get; set; }
        
        public DateTime RegisterDateTimeUtc { get; set; }
        
        public long TeamId { get; set; }
        
        public virtual TeamEntity Team { get; set; }
        
        public virtual ICollection<HistoryEntryEntity> Reviews { get; set; }
    }
}