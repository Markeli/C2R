using System;
using C2R.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C2R.Core.Data.Configurations
{
    public class TeamConfigEntityConfiguration : IEntityTypeConfiguration<TeamConfigEntity>
    {
        public void Configure(EntityTypeBuilder<TeamConfigEntity> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ToTable("TeamConfigs");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.CodeReviewerProvidingStrategy)
                .HasColumnName("CodeReviewerProvidingStrategy")
                .IsRequired();
            
            builder
                .Property(x => x.CommunicationMode)
                .HasColumnName("CommunicationMode")
                .IsRequired();

            builder
                .Property(x => x.ReminderJobId)
                .HasColumnName("ReminderJobId");
            
            builder
                .Property(x => x.RemindTimeUtc)
                .HasColumnName("RemindTimeUtc")
                .IsRequired();
        }
    }
}