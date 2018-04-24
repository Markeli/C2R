using System;
using System.Linq;
using System.Threading.Tasks;
using C2R.Core.Contracts;
using JetBrains.Annotations;

namespace C2R.Core.ReviewerProviderStrategies
{
    public class RoundRobinCodeReviewerProviderStrategy : ICodeReviewerProviderStrategy
    {

        public Guid StrategyId { get; } = CodeReviewerProviderStrategiesIds.RoundRobin;

        [NotNull]
        private readonly ICodeReviewHistoryService _historyService;
        
        
        public RoundRobinCodeReviewerProviderStrategy([NotNull] ICodeReviewHistoryService historyService)
        {
            _historyService = historyService;
        }
        
        public async Task<TeamMember> GetCodeReviewerAsync(Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            switch (team.Members.Count)
            {
                case 0:
                    return null;
                case 1:
                    return team.Members.First();
                default:
                    var lastReviewer = await _historyService
                        .GetLastReviewerAsync(team.Id)
                        .ConfigureAwait(false);

                    var orderedByDateMembers = team.Members
                        .OrderBy(x => x.RegisterDateTimeUtc)
                        .ToList();

                    if (lastReviewer == null) return orderedByDateMembers.First();

                    var temp = orderedByDateMembers.FirstOrDefault(x => x.Id == lastReviewer.Id);
                    if (temp == null) return orderedByDateMembers.First();
                    var lastReviewerIndex = orderedByDateMembers.IndexOf(temp);

                    if (lastReviewerIndex + 1 < orderedByDateMembers.Count)
                        return orderedByDateMembers[lastReviewerIndex + 1];

                    return orderedByDateMembers.First();
            }
        }
    }
}