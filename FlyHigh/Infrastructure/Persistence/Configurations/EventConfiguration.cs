using Domain.Events;
using Domain.Matches;
using Domain.Teams;
using Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations;

internal sealed class EventConfiguration
  : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.ToTable("Events");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id)
      .HasConversion(
      id => id.Value,
      value => new EventId(value));

    builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

    builder.Property(e => e.Description)
        .HasMaxLength(1000);

    builder.Property(e => e.Type)
        .HasConversion<string>()
        .IsRequired();

    builder.Property(e => e.CreatedAt)
        .IsRequired();

    builder.Property(e => e.TeamId)
            .HasConversion(
                id => id.Value,
                value => new TeamId(value))
            .IsRequired();

    builder.Property(e => e.CreatedBy)
        .HasConversion(
            id => id.Value,
            value => new UserId(value))
        .IsRequired();

    builder.Property(e => e.MatchId)
       .HasConversion(
           id => id != null ? id.Value : (Guid?)null,
           value => value != null ? new MatchId(value.Value) : null)
       .IsRequired(false);

    builder.HasMany(e => e.Participants)
        .WithOne()
        .HasForeignKey("EventId")
        .OnDelete(DeleteBehavior.Cascade);
  }
}
