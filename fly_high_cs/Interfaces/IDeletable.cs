namespace fly_high_cs.Interfaces
{
    public interface IDeletable
    {
        DateTime? DeletedAt { get; set; }
        int? DeletedByUserId { get; set; }
    }
}
