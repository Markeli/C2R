using System;
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
    public class TeamService : ITeamService
    {
        [NotNull]
        private readonly IC2RDataContextFactory _dataContextFactory;

        public TeamService([NotNull] IC2RDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory ?? throw new ArgumentNullException(nameof(dataContextFactory));
        }

        public async Task<long> CreateTeamAsync(Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            var entity = team.ToEntity();

            using (var context = _dataContextFactory.Create())
            {
                var result = await context.Set<TeamEntity>()
                    .AddAsync(entity)
                    .ConfigureAwait(false);

                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
                
                return result.Entity.Id;
            }
        }

        public async Task DeleteTeamAsync(long teamId)
        { 
            using (var context = _dataContextFactory.Create())
            {
                var entity = await context.Set<TeamEntity>()
                    .FirstOrDefaultAsync(x => x.Id == teamId)
                    .ConfigureAwait(false);
                if (entity == null) throw new ArgumentException($"Team with id {teamId} not found");

                context.Set<TeamEntity>().Remove(entity);

                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        public async Task<bool> IsTeamRegisteredAsync(long telegramChatId)
        { 
            using (var context = _dataContextFactory.Create())
            {
                var entity = await context.Set<TeamEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.TelegramChatId == telegramChatId)
                    .ConfigureAwait(false);

                return entity != null;
            }
        }

        public async Task<Team> GetTeamAsync(long telegramChatId)
        {
            using (var context = _dataContextFactory.Create())
            {
                var entity = await context.Set<TeamEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.TelegramChatId == telegramChatId)
                    .ConfigureAwait(false);

                if (entity == null) throw new ArgumentException($"Team with telegramChatId {telegramChatId} not found");

                return entity.ToDomain();
            }
        }

        public async Task AddTeamMemberAsync(long teamId, TeamMember teamMember)
        {
            if (teamMember == null) throw new ArgumentNullException(nameof(teamMember));
            
            using (var context = _dataContextFactory.Create())
            {
                var entity = teamMember.ToEntity();
                entity.TeamId = teamId;

                await context.Set<TeamMemberEntity>()
                    .AddAsync(entity)
                    .ConfigureAwait(false);
            }
        }

        public async Task RemoveTeamMemberAsync(long teamId, long teamMemberId)
        {
            using (var context = _dataContextFactory.Create())
            {
                var entity = await context.Set<TeamMemberEntity>()
                    .FirstOrDefaultAsync(x => x.Id == teamMemberId && x.TeamId == teamId)
                    .ConfigureAwait(false);

                if (entity == null)
                    throw new ArgumentException(
                        $"Team member with id {teamMemberId} not found in team with id {teamId}");

                context.Set<TeamMemberEntity>()
                    .Remove(entity);

                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}