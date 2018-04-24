using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace C2R.Core.Contracts
{
    public interface ICodeReviewerProviderStrategy
    {
        Guid StrategyId { get; }
        
        [NotNull]
        Task<TeamMember> GetCodeReviewerAsync([NotNull] Team team);
    }
}