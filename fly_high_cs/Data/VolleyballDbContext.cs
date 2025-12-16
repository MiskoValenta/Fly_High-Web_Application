using fly_high_cs.helpers;
using fly_high_cs.Interfaces;
using fly_high_cs.Models;
using fly_high_cs.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace fly_high_cs.Data
{
    public class VolleyballDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchRoster> MatchRosters { get; set; }
        public DbSet<MatchSet> MatchSets { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public VolleyballDbContext(DbContextOptions<VolleyballDbContext> options,ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var events = modelBuilder.Entity<Event>();
            var eventParticipants = modelBuilder.Entity<EventParticipant>();
            var matches = modelBuilder.Entity<Match>();
            var matchRosters = modelBuilder.Entity<MatchRoster>();
            var matchSets = modelBuilder.Entity<MatchSet>();
            var teams = modelBuilder.Entity<Team>();
            var teamMembers = modelBuilder.Entity<TeamMember>();
            var users = modelBuilder.Entity<User>();
            var auditLogs = modelBuilder.Entity<AuditLog>();

            users.HasIndex(u => u.Email).IsUnique();
            users.HasQueryFilter(u => !u.IsDeleted);

            teams.HasIndex(t => t.ShortName).IsUnique();
            teams.Property(t => t.ShortName).HasMaxLength(6);
            teams.HasQueryFilter(t => !t.IsDeleted);

            teamMembers.HasIndex(tm => new { tm.TeamId, tm.UserId }).IsUnique();
            teamMembers.HasOne(tm => tm.Team).WithMany(t => t.Members).HasForeignKey(tm => tm.TeamId);
            teamMembers.HasOne(tm => tm.User).WithMany(u => u.TeamMembers).HasForeignKey(tm => tm.UserId);
            teamMembers.HasQueryFilter(tm => !tm.IsDeleted);

            matches.HasOne(m => m.HomeTeam).WithMany(t => t.HomeMatches).HasForeignKey(m => m.HomeTeamId).OnDelete(DeleteBehavior.Restrict);
            matches.HasOne(m => m.AwayTeam).WithMany(t => t.AwayMatches).HasForeignKey(m => m.AwayTeamId).OnDelete(DeleteBehavior.Restrict);
            matches.HasQueryFilter(m => !m.IsDeleted);

            matchSets.HasIndex(ms => new { ms.MatchId, ms.SetNumber}).IsUnique();
            
            matchRosters.HasIndex(mr => new { mr.MatchId, mr.TeamMemberId }).IsUnique();

            events.HasOne(e => e.CreatedByUser).WithMany(u => u.CreatedEvents).HasForeignKey(e => e.CreatedByUserId);
            events.HasQueryFilter(e => !e.IsDeleted);

            eventParticipants.HasIndex(ep => new { ep.EventId, ep.TeamMemberId }).IsUnique();

            auditLogs.Property(al => al.EntityName).IsRequired().HasMaxLength(100);
            auditLogs.Property(al => al.Action).IsRequired();
            auditLogs.HasIndex(al => al.EntityName);
            auditLogs.HasIndex(al => al.EntityId);
        }
        public override int SaveChanges()
        {
            ApplyAuditAndSoftDelete();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            ApplyAuditAndSoftDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }


        private void ApplyAuditAndSoftDelete()
        {
            // Soft delete & audit dohromady -> aka. nic se nemůže smazat "Hard Delete"
            ChangeTracker.DetectChanges();
            var now = DateTime.UtcNow;
            var userId = _currentUserService?.UserId;

            var auditEntries = new List<AuditLog>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not BaseEntity entity || entry.Entity is AuditLog)
                    continue;

                // Soft delete
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                    entity.DeletedAt = now;
                    entity.DeletedByUserId = userId;
                }

                // Created / Updated audit
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                    if (userId.HasValue)
                    {
                        entity.CreatedByUserId = userId.Value;
                    }
                    else
                    {
                        throw new InvalidOperationException("Nelze nastavit CreatedByUserId na null.");
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = now;
                    entity.UpdatedByUserId = userId;
                }

                // Audit log
                var audit = new AuditLog
                {
                    EntityName = entry.Entity.GetType().Name,
                    EntityId = entity.Id,
                    Action = entry.State switch
                    {
                        EntityState.Added => AuditActions.Create,
                        EntityState.Modified => AuditActions.Update,
                        EntityState.Deleted => AuditActions.Delete,
                        _ => throw new NotSupportedException()
                    },
                    UserId = userId,
                    CreatedAt = now,
                    OldValues = entry.State == EntityState.Modified ? SerializeValues(entry.OriginalValues) : null,
                    NewValues = entry.State == EntityState.Modified ? SerializeValues(entry.CurrentValues) : null
                };

                auditEntries.Add(audit);
            }

            if (auditEntries.Any())
                AuditLogs.AddRange(auditEntries);
        }

        private static string SerializeValues(PropertyValues values)
        {
            var dict = values.Properties.ToDictionary(
                p => p.Name,
                p => values[p]?.ToString()
            );

            return JsonSerializer.Serialize(dict);
        }
    }
}
