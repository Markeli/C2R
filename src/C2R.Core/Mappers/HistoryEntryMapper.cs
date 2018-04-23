using C2R.Core.Contracts;
using C2R.Core.Data.Entities;

namespace C2R.Core.Mappers
{
    public static class HistoryEntryMapper
    {
        public static HistoryEntry ToDomain(this HistoryEntryEntity entity)
        {
            if (entity == null) return null;

            return new HistoryEntry
            {
                Id = entity.Id,
                TeamId = entity.TeamId,
                ReviewDateTimeUtc = entity.ReviewDateTimeUtc,
                ReviewedTeamMemberId = entity.ReviewedTeamMemberId
            };
        }

        public static HistoryEntryEntity ToEntity(this HistoryEntry config)
        {
            if (config == null) return null;

            return new HistoryEntryEntity
            {
                Id = config.Id,
                TeamId = config.TeamId,
                ReviewDateTimeUtc = config.ReviewDateTimeUtc,
                ReviewedTeamMemberId = config.ReviewedTeamMemberId
            };
        }
    }
}