using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public class CodeReviewerResponse
    {
        public CodeReviewerResponse([CanBeNull] TeamMember codeReviwer, bool isTodaySelected = false)
        {
            CodeReviwer = codeReviwer;
            IsTodaySelected = isTodaySelected;
        }

        [CanBeNull]
        public TeamMember CodeReviwer { get; }
        
        public bool IsTodaySelected { get; }
    }
}