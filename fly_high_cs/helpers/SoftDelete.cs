using fly_high_cs.Interfaces;

namespace fly_high_cs.helpers
{
    public static class SoftDeleteExtensions 
    {
        public static void SoftDelete(this ISoftDelete entity)
        {
            if (entity.IsDeleted)
                return;

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
        }
    }
}
