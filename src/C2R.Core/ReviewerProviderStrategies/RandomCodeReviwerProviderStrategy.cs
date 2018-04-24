using System;
using System.Linq;
using System.Threading.Tasks;
using C2R.Core.Contracts;

namespace C2R.Core.ReviewerProviderStrategies
{
    public class RandomCodeReviwerProviderStrategy : IRandomCodeReviewerProviderStrategy
    {
        public Guid StrategyId { get; } = DefaultCodeReviewerProviderStrategy.RandomStrategyId;
        
        public Task<TeamMember> GetCodeReviewerAsync(Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));
            
            var count = team.Members.Count;
            switch (count)
            {
                case 0:
                    return Task.FromResult(default(TeamMember));
                case 1:
                    return Task.FromResult(team.Members.First());
                default:
                    var randomizer = new Random();
                    var reviewerOrder = randomizer.Next(0, count -1);
                    return Task.FromResult(team.Members.ToArray()[reviewerOrder]);
            }
        }
    }
}