using C2R.Core.Contracts;
using C2R.Core.Data.Entities;

namespace C2R.Core.Mappers
{
    public static  class TeamMemberMapper
    {
        public static TeamMember ToDomain(this TeamMemberEntity entity)
        {
            if (entity == null) return null;
            
            return new TeamMember
            {
                Id = entity.Id,
                TelegramUserId = entity.TelegramUserId,
                TelegramUsername = entity.TelegramUsername
            };
        }

        public static TeamMemberEntity ToEntity(this TeamMember member)
        {
            if (member == null) return null;
            
            return new TeamMemberEntity
            {
                Id = member.Id,
                TelegramUserId = member.TelegramUserId,
                TelegramUsername = member.TelegramUsername
            };
        }
    }
}