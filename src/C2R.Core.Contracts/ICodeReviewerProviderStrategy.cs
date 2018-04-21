using System;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewerProviderStrategy
    {
        Guid StrategyId { get; }
        
        [CanBeNull]
        TeamMember GetCodeReviewer([NotNull] Team team);
    }
}