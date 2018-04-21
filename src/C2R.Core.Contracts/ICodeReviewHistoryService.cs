namespace C2R.Core.Contracts
{
    public interface ICodeReviewHistoryService
    {
        void AddReview(HistoryEntry entry);

        void RemoveLastReview(long teamId);
    }
}