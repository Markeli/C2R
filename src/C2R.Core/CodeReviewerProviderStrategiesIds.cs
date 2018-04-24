using System;
using C2R.Core.ReviewerProviderStrategies;

namespace C2R.Core
{
    public class CodeReviewerProviderStrategiesIds
    {
        public static readonly Guid RoundRobin = Guid.Parse("80d17969-eba6-43ed-a45a-87ef14b9023e");

        public static readonly Guid Random = DefaultCodeReviewerProviderStrategy.RandomStrategyId;
    }
}