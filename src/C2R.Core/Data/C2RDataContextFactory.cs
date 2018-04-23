using System;
using Microsoft.EntityFrameworkCore;

namespace C2R.Core.Data
{
    public class C2RDataContextFactory : IC2RDataContextFactory
    {
        private readonly DbContextOptions<C2RDataContext> _options;
        
        public C2RDataContextFactory(DbContextOptions<C2RDataContext> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public C2RDataContext Create()
        {
            var context = new C2RDataContext(_options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}