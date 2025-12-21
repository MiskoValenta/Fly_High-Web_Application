using Microsoft.EntityFrameworkCore;
using Domain.Users;
using Domain.Teams;

namespace Infrastructure.Persistence;

public class VolleyballDbContext : DbContext
{
  public DbSet<User> Users => Set<User>();
  public DbSet<Team> Teams => Set<Team>();

  public VolleyballDbContext(DbContextOptions<VolleyballDbContext> options)
    : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    var user = modelBuilder.Entity<User>();

    user.HasKey(u => u.Id);
    user.HasIndex(u => u.Email).IsUnique();
    user.Property(u => u.HashedPassword).IsRequired();
  }
}
