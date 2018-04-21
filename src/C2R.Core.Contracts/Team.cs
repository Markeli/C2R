using System.Collections.Generic;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public class Team
    {
        public long Id { get; set; }
        
        public long TelegramChatId { get; set; }
        
        [NotNull]
        [ItemNotNull]
        public IReadOnlyCollection<TeamMember> Members { get; set; }
    }
}