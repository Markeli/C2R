using System.Collections.Generic;

namespace C2R.Core.Data.Entities
{
    public class TeamEntity
    {
        public long Id { get; set; }
        
        public long TelegramChatId { get; set; }
        
        public virtual TeamConfigEntity TeamConfig { get; set; }
        
        public virtual ICollection<TeamMemberEntity> Members { get; set; }
        
        public virtual ICollection<HistoryEntryEntity> HistoryEntries { get; set; }
    }
}