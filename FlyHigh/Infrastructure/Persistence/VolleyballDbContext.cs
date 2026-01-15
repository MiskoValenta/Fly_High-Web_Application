using Microsoft.EntityFrameworkCore;
using Domain.Users;
using Domain.Teams;
using Domain.Events;
using Domain.Matches;
using Domain.TeamMembers;

namespace Infrastructure.Persistence;

public class VolleyballDbContext : DbContext
{
  public DbSet<User> Users => Set<User>();
  public DbSet<Team> Teams => Set<Team>();
  public DbSet<Event> Events => Set<Event>();
  public DbSet<Match> Matches => Set<Match>();
  public DbSet<MatchRosterEntry> MatchRosterEntries => Set<MatchRosterEntry>();
  public DbSet<EventParticipant> EventParticipants => Set<EventParticipant>();
  public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
  public DbSet<MatchSet> MatchSets => Set<MatchSet>();

  public VolleyballDbContext(DbContextOptions<VolleyballDbContext> options)
    : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolleyballDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}
