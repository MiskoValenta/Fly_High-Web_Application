using fly_high_cs.Models.Enums;

namespace fly_high_cs.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        public string EntityName { get; set; }
        public int EntityId { get; set; }

        public AuditActions Action { get; set; }

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }

        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
