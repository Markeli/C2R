using System;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewerProvider
    {
        void RegisterStrategy(ICodeReviewerProviderStrategy strategy);

        void UnregisterStrategy(Guid strategyId);
        
        TeamMember GetCodeReviewer(Team team, ReminderConfig config);
    }
}