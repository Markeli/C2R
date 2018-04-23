using JetBrains.Annotations;

namespace C2R.Core.Data
{
    public interface IC2RDataContextFactory
    {
        [NotNull]
        C2RDataContext Create();
    }
}