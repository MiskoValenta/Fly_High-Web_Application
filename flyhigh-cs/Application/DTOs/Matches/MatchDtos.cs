using Domain.Entities.Matches.MatchEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Matches;

public class CreateMatchDto
{
  public Guid HomeTeamId { get; set; }
  public Guid AwayTeamId { get; set; }
  public DateTime ScheduledAt { get; set; }
  public string Location { get; set; } = string.Empty;
}

public class AssignPositionDto
{
  public int SetNumber { get; set; }
  public Guid TeamMemberId { get; set; }
  public PlayerPosition Position { get; set; }
}

public class AddRosterEntryDto
{
  public Guid TeamMemberId { get; set; }
  public Guid TeamId { get; set; }
  public int JerseyNumber { get; set; }
}

public class CancelMatchDto
{
  public string Reason { get; set; } = string.Empty;
}
