using System;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewerProviderStrategy
    {
        Guid StrategyId { get; }
        
        [NotNull]
        CodeReviewerResponse GetCodeReviewer([NotNull] Team team, bool ignoreHistory);
    }
}