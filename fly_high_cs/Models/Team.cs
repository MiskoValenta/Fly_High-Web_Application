using fly_high_cs.Interfaces;

namespace fly_high_cs.Models
{
    public class Team : BaseEntity
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime createdAt { get; set; }

        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
