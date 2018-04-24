using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewerProvider
    {
        void RegisterStrategy([NotNull] ICodeReviewerProviderStrategy strategy);

        void UnregisterStrategy(Guid strategyId);
        
        [NotNull]
        Task<CodeReviewerResponse> GetCodeReviewerAsync(
            [NotNull] Team team, 
            Guid reviewerProviderStrategyId, 
            bool returnTodaySelectedReviewer = true);
    }
}