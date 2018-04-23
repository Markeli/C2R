using System;
using C2R.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C2R.Core.Data.Configurations
{
    public class TeamEntityConfiguration : IEntityTypeConfiguration<TeamEntity>
    {
        public void Configure(EntityTypeBuilder<TeamEntity> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            builder
                .ToTable("Teams");
            
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(x => x.TelegramChatId)
                .HasColumnName("TelegramChatId")
                .IsRequired();

            builder
                .HasIndex(x => x.TelegramChatId)
                .IsUnique();
            
            builder
                .HasMany(x => x.HistoryEntries)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId);
            
            
            builder
                .HasMany(x => x.Members)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId);

            builder
                .HasOne(x => x.TeamConfig)
                .WithOne(x => x.Team)
                .HasForeignKey<TeamConfigEntity>(x => x.TeamId);
        }
    }
}