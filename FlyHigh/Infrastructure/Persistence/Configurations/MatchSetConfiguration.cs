using Domain.Matches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations;

internal sealed class MatchSetConfiguration
  : IEntityTypeConfiguration<MatchSet>
{
  public void Configure(EntityTypeBuilder<MatchSet> builder)
  {
    builder.ToTable("MatchSets");

    builder.HasKey(ms => ms.Id);

    builder.Property(ms => ms.Id)
      .HasConversion(
      id => id.Value,
      value => new MatchSetId(value));

    builder.Property(ms => ms.SetNumber)
      .IsRequired();

    builder.Property(ms => ms.HomeScore)
      .IsRequired();

    builder.Property(ms => ms.AwayScore)
      .IsRequired();

    builder.Property(ms => ms.Winner)
      .HasConversion<string>();

    builder.HasOne<Match>()
      .WithMany(ms => ms.Sets)
      .HasForeignKey("MatchId")
      .OnDelete(DeleteBehavior.Cascade);
  }
}
