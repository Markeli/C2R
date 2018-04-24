using System;
using C2R.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C2R.Core.Data.Configurations
{
    public class TeamMemberEntityConfiguration : IEntityTypeConfiguration<TeamMemberEntity>
    {
        public void Configure(EntityTypeBuilder<TeamMemberEntity> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));


            builder.ToTable("TeamMembers");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.TelegramUserId)
                .HasColumnName("TelegramUserId")
                .IsRequired();

            builder
                .Property(x => x.TelegramUsername)
                .HasColumnName("TelegramUsername")
                .IsRequired();

            builder
                .HasMany(x => x.Reviews)
                .WithOne(x => x.ReviewedTeamMember)
                .HasForeignKey(x => x.ReviewedTeamMemberId);

            builder
                .HasOne(x => x.Team)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.TeamId);

        }
    }
}