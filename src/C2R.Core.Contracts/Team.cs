using System.Collections.Generic;

namespace C2R.Core.Contracts
{
    public class Team
    {
        public long Id { get; set; }
        
        public long TelegramChatId { get; set; }
        
        public ICollection<TeamMember> Members { get; set; }
    }
}