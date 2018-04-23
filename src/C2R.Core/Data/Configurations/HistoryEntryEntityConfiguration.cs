using System;
using C2R.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C2R.Core.Data.Configurations
{
    public class HistoryEntryEntityConfiguration : IEntityTypeConfiguration<HistoryEntryEntity>
    {
        public void Configure(EntityTypeBuilder<HistoryEntryEntity> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ToTable("HistoryEntries");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.ReviewDateTimeUtc)
                .HasColumnName("ReviewDateTimeUtc")
                .IsRequired();
            
        }
    }
}