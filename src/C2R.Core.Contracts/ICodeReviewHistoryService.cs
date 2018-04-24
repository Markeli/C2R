using System.Threading.Tasks;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewHistoryService
    {
        [NotNull]
        Task AddReviewAsync([NotNull] HistoryEntry entry);

        [NotNull]
        Task<HistoryEntry> GetLastReviewerAsync(long teamId);
        
        [NotNull]
        Task RemoveLastReviewAsync(long teamId);
    }
}