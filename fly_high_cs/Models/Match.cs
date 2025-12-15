using fly_high_cs.Models.Enums;

namespace fly_high_cs.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateOnly ScheduleAt { get; set; }
        public string RefereeName { get; set; }
        public string Notes { get; set; }
        public Statuses Status { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
