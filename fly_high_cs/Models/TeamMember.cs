using fly_high_cs.Interfaces;
using fly_high_cs.Models.Enums;

namespace fly_high_cs.Models
{
    public class TeamMember : BaseEntity
    {
        public int TeamMemberId { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public Roles Role { get; set; }
        public DateTime JoinedAt { get; set; }

        public Team Team { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<MatchRoster> MatchRosters { get; set; } = new List<MatchRoster>();
        public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}
