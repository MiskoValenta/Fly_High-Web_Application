using fly_high_cs.Models.Enums;

namespace fly_high_cs.Models
{
    public class EventParticipant
    {
        public int EventParticipantId { get; set; }
        public int EventId { get; set; }
        public int TeamMemberId { get; set; }
        public Responses Response { get; set; }
    }
}
