namespace fly_high_cs.Interfaces
{
    public interface IAuditable
    {
        DateTime CreatedAt { get; set; }
        int CreatedByUserId { get; set; }

        DateTime? UpdatedAt { get; set; }
        int? UpdatedByUserId { get; set; }
    }
}
