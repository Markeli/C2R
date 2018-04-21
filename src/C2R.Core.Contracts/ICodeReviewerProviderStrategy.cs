using System;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewerProviderStrategy
    {
        Guid StrategyId { get; }
        
        TeamMember GetCodeReviewer(Team team);
    }
}