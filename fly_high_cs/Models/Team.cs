namespace fly_high_cs.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public char[] ShortName { get; set; }
        public string Description { get; set; }
        public DateOnly createdAt { get; set; }
    }
}
