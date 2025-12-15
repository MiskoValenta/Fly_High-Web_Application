using fly_high_cs.Models.Enums;

namespace fly_high_cs.Models
{
    public class MatchRoster
    {
        public int MatchRosterId { get; set; }
        public int MatchId { get; set; }
        public int TeamMemberId { get; set; }
        public Positions Position { get; set; }
    }
}
