using System.Collections.Generic;
using System.Linq;
using C2R.Core.Contracts;
using C2R.Core.Data.Entities;

namespace C2R.Core.Mappers
{
    public static  class TeamMapper
    {
        public static Team ToDomain(this TeamEntity entity)
        {
            if (entity == null) return null;

            return new Team
            {
                Id = entity.Id,
                TelegramChatId = entity.TelegramChatId,
                Members = entity.Members == null
                    ? new List<TeamMember>(0)
                    : entity.Members
                        .Select(x => x.ToDomain())
                        .ToList()
            };
        }

        public static TeamEntity ToEntity(this Team team)
        {
            if (team == null) return null;
            
            return new TeamEntity
            {
                Id = team.Id,
                TelegramChatId = team.TelegramChatId,
                Members = team.Members.Select(x => x.ToEntity()).ToList()
            };
        }
    }
}