using System;

namespace C2R.Core.Contracts
{
    public class TeamMember
    {
        public long Id { get; set; }
        
        public long TelegramUserId { get; set; }
        
        public DateTime? LastReviewTimeUtc { get; set; }
    }
}