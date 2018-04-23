using System.Threading.Tasks;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewHistoryService
    {
        Task AddReviewAsync([NotNull] HistoryEntry entry);

        Task RemoveLastReviewAsync(long teamId);
    }
}