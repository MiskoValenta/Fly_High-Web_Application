using fly_high_cs.Models;
using Microsoft.EntityFrameworkCore;

namespace fly_high_cs.Data
{
    public class VolleyballDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchRoster> MatchRosters { get; set; }
        public DbSet<MatchSet> MatchSets { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<User> Users { get; set; }

        public VolleyballDbContext(DbContextOptions<VolleyballDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
