using System.Collections.Generic;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public class Team
    {
        public Team(long id, long telegramChatId, [ItemNotNull] IReadOnlyCollection<TeamMember> members = null)
        {
            Id = id;
            TelegramChatId = telegramChatId;
            Members = members ?? new List<TeamMember>(0);
        }

        public long Id { get; set; }
        
        public long TelegramChatId { get; set; }
        
        [NotNull]
        [ItemNotNull]
        public IReadOnlyCollection<TeamMember> Members { get; set; }
    }
}