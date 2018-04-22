using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public class CodeReviewerResponse
    {
        [CanBeNull]
        public TeamMember CodeReviwer { get; }
        
        public bool IsTodaySelected { get; }
    }
}