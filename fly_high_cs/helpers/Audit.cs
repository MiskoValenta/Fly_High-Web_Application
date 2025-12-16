using fly_high_cs.Interfaces;

namespace fly_high_cs.helpers
{
    public static class Audit
    {
        public static void MarkAsCreated(this IAuditable entity, int userId)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedByUserId = userId;
        }

        public static void MarkAsUpdated(this IAuditable entity, int userId)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedByUserId = userId;
        }
    }
}
