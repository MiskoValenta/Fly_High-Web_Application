using Application.Common.Interfaces;
using Domain.Entities.Teams;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence;

public class FlyHighDbContext : DbContext, IUnitOfWork
{
  public FlyHighDbContext(DbContextOptions<FlyHighDbContext> options) : base(options) { }

  public DbSet<User> Users => Set<User>();
  public DbSet<Team> Teams => Set<Team>();
  public DbSet<TeamMember> TeamMembers => Set<TeamMember>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(
      typeof(FlyHighDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}
