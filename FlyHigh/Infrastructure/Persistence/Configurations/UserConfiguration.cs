using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration
  :IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");

    builder.HasKey(u => u.Id);

    builder.Property(u => u.Id)
      .HasConversion(
      id => id.Value,
      value => new UserId(value));

    builder.Property(u => u.FirstName)
      .IsRequired()
      .HasMaxLength(50);

    builder.Property(u => u.LastName)
      .IsRequired()
      .HasMaxLength(50);

    builder.Property(u => u.Email)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.HashedPassword)
      .IsRequired()
      .HasMaxLength(255);

    builder.Property(u => u.CreatedAt)
      .IsRequired();

    builder.Property(u => u.LastLogged)
      .IsRequired(false);

    builder.HasIndex(u => u.Email)
      .IsUnique();
  }
}
