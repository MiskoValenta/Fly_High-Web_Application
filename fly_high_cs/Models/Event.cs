using fly_high_cs.Interfaces;
using fly_high_cs.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;

namespace fly_high_cs.Models
{
    public class Event : BaseEntity
    {
        public int EventId { get; set; }
        public int TeamId { get; set; }
        public int? MatchId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Types Type { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }

        public Team Team { get; set; } = null!;
        public Match? Match { get; set; }
        public User CreatedByUser { get; set; } = null!;
        public ICollection<EventParticipant> Participants { get; set; } = new List<EventParticipant>();
    }
}
