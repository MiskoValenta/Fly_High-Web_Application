namespace fly_high_cs.Models.Enums
{
    public enum Responses
    {
        Yes,
        No,
        Ignored
    }
    public enum Types
    {
        Practice,
        Match,
        Other
    }
    public enum Statuses
    {
        Scheduled,
        Played,
        Cancelled
    }
    public enum Roles
    {
        Coach,
        Captain,
        Player
    } 
    public enum Positions
    {
        Setter,
        Spiker,
        Blocker,
        Universal,
        Libero
    }
    public enum  AuditActions
    {
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
