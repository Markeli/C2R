using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using JetBrains.Annotations;

namespace C2R.Core
{
    public class CodeReviewerProvider : ICodeReviewerProvider
    {
        [NotNull]
        private readonly Dictionary<Guid, ICodeReviewerProviderStrategy> _registeredStrategies;

        [NotNull]
        private readonly ICodeReviewHistoryService _codeReviewHistoryService;

        public CodeReviewerProvider([NotNull] ICodeReviewHistoryService codeReviewHistoryService)
        {
            _codeReviewHistoryService = codeReviewHistoryService ?? throw new ArgumentNullException(nameof(codeReviewHistoryService));
            _registeredStrategies = new Dictionary<Guid, ICodeReviewerProviderStrategy>();
        }
        
        public void RegisterStrategy(ICodeReviewerProviderStrategy strategy)
        {
            if (strategy == null) throw new ArgumentNullException(nameof(strategy));
            _registeredStrategies[strategy.StrategyId] = strategy;
        }

        public void UnregisterStrategy(Guid strategyId)
        {
            if (!_registeredStrategies.ContainsKey(strategyId)) throw new ArgumentException($"Strategy with id {strategyId} not registered");

            _registeredStrategies.Remove(strategyId);
        }

        public async Task<CodeReviewerResponse> GetCodeReviewerAsync(
            Team team,
            Guid reviewerProviderStrategyId,
            bool returnTodaySelectedReviewer = true)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            if (team.Members.Count == 0) return new CodeReviewerResponse(null);

            if (!_registeredStrategies.ContainsKey(reviewerProviderStrategyId))
                throw new InvalidOperationException($"Strategy with id {reviewerProviderStrategyId} not registered");

            var strategy = _registeredStrategies[reviewerProviderStrategyId];
            if (strategy == null) throw new InvalidOperationException($"Strategy with id {reviewerProviderStrategyId} is null");

            var lastReviewer = await _codeReviewHistoryService
                .GetLastReviewerAsync(team.Id)
                .ConfigureAwait(false);

            var isTodayReviewerSelected = false;

            if (lastReviewer != null)
            {
                isTodayReviewerSelected = Equals(lastReviewer.ReviewDateTimeUtc.Date, DateTime.UtcNow.Date);
            }

            if (returnTodaySelectedReviewer && isTodayReviewerSelected)
            {
                var codeReviewer = team.Members.FirstOrDefault(x => x.Id == lastReviewer.ReviewedTeamMemberId);

                if (codeReviewer != null)
                {
                    return new CodeReviewerResponse(codeReviewer);
                }
            }

            var reviewer = await strategy
                .GetCodeReviewerAsync(team)
                .ConfigureAwait(false);

            if (reviewer == null) return new CodeReviewerResponse(null);
            
            if (isTodayReviewerSelected)
            {
                await _codeReviewHistoryService
                    .RemoveLastReviewAsync(team.Id)
                    .ConfigureAwait(false);
            }
            
            var entry = new HistoryEntry
            {
                ReviewDateTimeUtc = DateTime.UtcNow,
                ReviewedTeamMemberId = reviewer.Id,
                TeamId = team.Id
            };
            await _codeReviewHistoryService.AddReviewAsync(entry).ConfigureAwait(false);
            
            return new CodeReviewerResponse(reviewer);
        }
    }
}