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
    public class TeamConfigService : ITeamConfigService
    {
        private Guid? _defaultProviderStrategy;
        private Guid? _defaultCommunicationMode;
        
        private readonly TimeSpan _defaultRemindTimeUtc = TimeSpan.FromHours(9);

        [NotNull]
        private readonly IC2RDataContextFactory _dataContextFactory;

        public TeamConfigService([NotNull] IC2RDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory ?? throw new ArgumentNullException(nameof(dataContextFactory));
        }

        public void SetDefaultProviderStrategy(Guid id)
        {
            _defaultProviderStrategy = id;
        }

        public void SetDefaultCommunicationMode(Guid id)
        {
            _defaultCommunicationMode = id;
        }

        public TeamConfig GetDefaultConfig()
        {
            if (!_defaultProviderStrategy.HasValue)
                throw new InvalidOperationException("Default provider strategy not setted");
            
            if (!_defaultCommunicationMode.HasValue)
                throw new InvalidOperationException("Default communication mode not setted");
            
            return new TeamConfig
            {
                CodeReviewerProvidingStrategyId = _defaultProviderStrategy.Value,
                CommunicationMode = _defaultCommunicationMode.Value,
                RemindTimeUtc = _defaultRemindTimeUtc,
            };
        }

        
        public async Task CreateConfigAsync(TeamConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            using (var context = _dataContextFactory.Create())
            {
                var entity = config.ToEntity();

               await context.Set<TeamConfigEntity>()
                   .AddAsync(entity)
                   .ConfigureAwait(false);

                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        public async Task<TeamConfig> GetConfigAsync(long teamId)
        { 
            using (var context = _dataContextFactory.Create())
            {
                var config = await context.Set<TeamConfigEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.TeamId == teamId)
                    .ConfigureAwait(false);

                if (config == null) throw new ArgumentException($"Team config with teamId {teamId} not found");

                return config.ToDomain();
            }
        }

        public async Task DeleteConfigAsync(long configId)
        {
            using (var context = _dataContextFactory.Create())
            {
                var config = await context.Set<TeamConfigEntity>()
                    .FirstOrDefaultAsync(x => x.Id == configId)
                    .ConfigureAwait(false);

                if (config == null) throw new ArgumentException($"Team config with id {configId} not found");

                context.Set<TeamConfigEntity>()
                    .Remove(config);

                await context.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        public async Task UpdateRemindTimeAsync(long teamId, TimeSpan remindTime)
        {
            using (var context = _dataContextFactory.Create())
            {
                var config = await context.Set<TeamConfigEntity>()
                    .FirstOrDefaultAsync(x => x.TeamId == teamId)
                    .ConfigureAwait(false);

                if (config == null) throw new ArgumentException($"Team config with teamId {teamId} not found");

                config.RemindTimeUtc = remindTime;

                await context.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
            
        }

    }
}