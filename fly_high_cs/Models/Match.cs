using fly_high_cs.Interfaces;
using fly_high_cs.Models.Enums;

namespace fly_high_cs.Models
{
    public class Match : BaseEntity
    {
        public int MatchId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime ScheduleAt { get; set; }
        public string? RefereeName { get; set; }
        public string? Notes { get; set; }
        public Statuses Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Team HomeTeam { get; set; } = null!;
        public Team AwayTeam { get; set; } = null!;
        public ICollection<MatchSet> Sets { get; set; } = new List<MatchSet>();
        public ICollection<MatchRoster> Rosters { get; set; } = new List<MatchRoster>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
