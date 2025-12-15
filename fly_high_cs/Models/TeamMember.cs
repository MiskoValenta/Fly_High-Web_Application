using fly_high_cs.Models.Enums;

namespace fly_high_cs.Models
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public Roles Role { get; set; }
        public DateOnly JoinedAt { get; set; }
    }
}
