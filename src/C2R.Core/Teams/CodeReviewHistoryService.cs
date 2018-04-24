using System;
using System.Linq;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using C2R.Core.Data;
using C2R.Core.Data.Abstract;
using C2R.Core.Data.Entities;
using C2R.Core.Mappers;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace C2R.Core.Teams
{
    public class CodeReviewHistoryService : ICodeReviewHistoryService
    {
        [NotNull]
        private readonly IC2RDataContextFactory _dataContextFactory;

        public CodeReviewHistoryService([NotNull] IC2RDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory ?? throw new ArgumentNullException(nameof(dataContextFactory));
        }

        public async Task AddReviewAsync(HistoryEntry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            using (var context = _dataContextFactory.Create())
            {
                var entity = entry.ToEntity();

                await context.Set<HistoryEntryEntity>()
                    .AddAsync(entity)
                    .ConfigureAwait(false);

                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        public async Task<HistoryEntry> GetLastReviewerAsync(long teamId)
        {
            using (var context = _dataContextFactory.Create())
            {
                var entity = await context.Set<HistoryEntryEntity>()
                    .AsNoTracking()
                    .OrderBy(x => x.ReviewDateTimeUtc)
                    .LastOrDefaultAsync(x => x.TeamId == teamId)
                    .ConfigureAwait(false);

                return entity?.ToDomain();
            }
        }

        public async  Task RemoveLastReviewAsync(long teamId)
        { 
            using (var context = _dataContextFactory.Create())
            {
                var entity = await context.Set<HistoryEntryEntity>()
                    .OrderBy(x => x.ReviewDateTimeUtc)
                    .LastOrDefaultAsync(x => x.TeamId == teamId)
                    .ConfigureAwait(false);
                
                if (entity == null) return;

                context.Set<HistoryEntryEntity>().Remove(entity);
                
                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}