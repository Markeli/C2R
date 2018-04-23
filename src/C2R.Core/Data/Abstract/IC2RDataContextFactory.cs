using JetBrains.Annotations;

namespace C2R.Core.Data.Abstract
{
    public interface IC2RDataContextFactory
    {
        [NotNull]
        C2RDataContext Create();
    }
}