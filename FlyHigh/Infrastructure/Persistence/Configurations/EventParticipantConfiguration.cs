using Domain.Events;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations;

internal sealed class EventParticipantConfiguration
  : IEntityTypeConfiguration<EventParticipant>
{
  public void Configure(EntityTypeBuilder<EventParticipant> builder)
  {
    builder.ToTable("EventParticipants");

    builder.HasKey(ep => ep.UserId);

    builder.Property(ep => ep.UserId)
      .HasConversion(
      id => id.Value,
      value => new UserId(value));

    builder.Property(ep => ep.Response)
      .HasConversion<string>()
      .IsRequired();

    builder.Property(ep => ep.AddedAt)
      .IsRequired();

    builder.Property(ep => ep.RespondedAt)
      .IsRequired(false);
  }
}
