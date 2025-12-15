using fly_high_cs.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;

namespace fly_high_cs.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Types Type { get; set; }
        public int CreatedByUserId { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly Deadline { get; set; }
    }
}
