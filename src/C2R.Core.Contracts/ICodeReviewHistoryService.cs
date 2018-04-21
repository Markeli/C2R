using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewHistoryService
    {
        void AddReview([NotNull] HistoryEntry entry);

        void RemoveLastReview(long teamId);
    }
}