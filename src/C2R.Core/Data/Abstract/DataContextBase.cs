using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace C2R.Core.Data.Abstract
{
    public abstract class DataContextBase<TContext> : DbContext
        where TContext : DbContext
    {
        public DataContextBase(DbContextOptions<TContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var applyGenericMethod =
                typeof(ModelBuilder).GetMethod("ApplyConfiguration", BindingFlags.Instance | BindingFlags.Public);


            var configurationTypes = typeof(TContext).Assembly.DefinedTypes
                .Where(x => x.ImplementedInterfaces.Any(y => y.IsGenericType
                                                             && y.GetGenericTypeDefinition() ==
                                                             typeof(IEntityTypeConfiguration<>)));

            foreach (var type in configurationTypes)
            {
                var baseInterface = type.ImplementedInterfaces
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
                var entityType = baseInterface.GenericTypeArguments[0];
                var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(entityType);
                applyConcreteMethod.Invoke(modelBuilder, new[] {Activator.CreateInstance(type)});
            }
        }
    }
}