using C2R.Core.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace C2R.Core.Data
{
    public class C2RDataContext : DataContextBase<C2RDataContext>
    {
        public C2RDataContext(DbContextOptions<C2RDataContext> options) : base(options)
        {
        }
    }
}