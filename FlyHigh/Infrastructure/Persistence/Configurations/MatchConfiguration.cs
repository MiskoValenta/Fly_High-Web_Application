using Domain.Matches;
using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations;

internal sealed class MatchConfiguration
: IEntityTypeConfiguration<Match>
{
  public void Configure(EntityTypeBuilder<Match> builder)
  {
    builder.ToTable("Matches");

    builder.HasKey(m => m.Id);

    builder.Property(m => m.Id)
      .HasConversion(
      id => id.Value,
      value => new MatchId(value));

    builder.Property(m => m.HomeTeamId)
      .HasConversion(
      id => id.Value,
      value => new TeamId(value));

    builder.Property(m => m.AwayTeamId)
      .HasConversion(
      id => id.Value,
      value => new TeamId(value));

    builder.Property(m => m.ScheduledAt)
      .IsRequired();

    builder.Property(m => m.RefereeName)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(m => m.Notes)
      .HasMaxLength(1000)
      .IsRequired(false);

    builder.Property(m => m.Status)
      .HasConversion<string>()
      .IsRequired();
  }
}
