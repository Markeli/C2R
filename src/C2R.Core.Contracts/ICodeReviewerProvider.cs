using System;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewerProvider
    {
        void RegisterStrategy([NotNull] ICodeReviewerProviderStrategy strategy);

        void UnregisterStrategy(Guid strategyId);
        
        [NotNull]
        CodeReviewerResponse GetCodeReviewer([NotNull] Team team, [NotNull] ReminderConfig config, bool ignoreHistory);
    }
}