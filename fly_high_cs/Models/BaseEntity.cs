using fly_high_cs.Interfaces;

namespace fly_high_cs.Models
{
    public abstract class BaseEntity : ISoftDelete, IAuditable, IDeletable
    {
        public int Id { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedByUserId { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public int CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedByUserId { get; set; }
    }
}
