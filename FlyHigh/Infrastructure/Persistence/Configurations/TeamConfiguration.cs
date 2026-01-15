using Domain.Matches;
using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations;

internal sealed class TeamConfiguration
  : IEntityTypeConfiguration<Team>
{
  public void Configure(EntityTypeBuilder<Team> builder)
  {
    builder.ToTable("Teams");

    builder.HasKey(t => t.Id);

    builder.Property(t => t.Id)
        .HasConversion(
        id => id.Value, 
        value => new TeamId(value));

    builder.Property(t => t.TeamName)
        .IsRequired()
        .HasMaxLength(30);

    builder.Property(t => t.ShortName)
        .IsRequired()
        .HasMaxLength(6);

    builder.Property(t => t.Description)
        .IsRequired(false)
        .HasMaxLength(1000);
  }
}
