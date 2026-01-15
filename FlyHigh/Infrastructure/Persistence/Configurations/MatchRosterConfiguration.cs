using Domain.Matches;
using Domain.TeamMembers;
using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations;

internal sealed class MatchRosterConfiguration
  : IEntityTypeConfiguration<MatchRosterEntry>
{
    public void Configure(EntityTypeBuilder<MatchRosterEntry> builder)
    {
      builder.HasKey(mre => mre.Id);

      builder.Property(mre => mre.Id)
        .HasConversion(
        id => id.Value,
        value => new MatchRosterEntryId(value));

      builder.Property(mre => mre.MatchId)
        .HasConversion(
          id => id.Value,
          value => new MatchId(value))
        .IsRequired();

      builder.Property(mre => mre.TeamId)
        .HasConversion(
          id => id.Value,
          value => new TeamId(value))
        .IsRequired();

      builder.Property(mre => mre.TeamMemberId)
        .HasConversion(
          id => id.Value,
          value => new TeamMemberId(value))
        .IsRequired();

      builder.HasOne<Match>()
        .WithMany(mre => mre.Roster)
        .HasForeignKey(mre => mre.MatchId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}


