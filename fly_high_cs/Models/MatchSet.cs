namespace fly_high_cs.Models
{
    public class MatchSet
    {
        public int MatchSetId { get; set; }
        public int MatchId { get; set; }
        public int SetNumber { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string Notes { get; set; }
    }
}
