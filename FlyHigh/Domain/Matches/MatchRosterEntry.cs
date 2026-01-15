using Domain.Common;
using Domain.Matches.MatchEnums;
using Domain.TeamMembers;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public class MatchRosterEntry : AuditableEntity<MatchRosterEntryId>
{
  public MatchId MatchId { get; private set; }
  public TeamMemberId TeamMemberId { get; private set; }
  public TeamId TeamId { get; private set; }
  public MatchPosition Position { get; private set; }
  public bool IsCaptain { get; private set; }
  public int JerseyNumber { get; private set; }

  internal MatchRosterEntry(
    MatchRosterEntryId id,
    MatchId matchId,
    TeamMemberId teamMemberId,
    TeamId teamId,
    MatchPosition position,
    int jerseyNumber,
    bool isCaptain = false) : base(id)
  {
    MatchId = matchId;
    TeamMemberId = teamMemberId;
    TeamId = teamId;
    Position = position;
    JerseyNumber = jerseyNumber;
    IsCaptain = isCaptain;
  }

  private MatchRosterEntry() { }

  public static MatchRosterEntry Create(
    MatchId matchId,
    TeamMemberId teamMemberId,
    TeamId teamId,
    MatchPosition position,
    int jerseyNumber)
  {
    return new MatchRosterEntry(
      MatchRosterEntryId.New(),
      matchId,
      teamMemberId,
      teamId,
      position,
      jerseyNumber);
  }
  internal void UpdatePosition(MatchPosition newPosition)
  {
    Position = newPosition;
  }
}
