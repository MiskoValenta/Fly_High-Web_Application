using Domain.Matches.MatchEnums;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public class MatchRosterEntry
{
  public MatchRosterEntryId Id { get; private set; }
  public TeamMemberId TeamMemberId { get; private set; }
  public MatchPosition Position { get; private set; }

  private MatchRosterEntry() { }

  internal MatchRosterEntry(TeamMemberId teamMemberId, MatchPosition position)
  {
    Id = MatchRosterEntryId.New();
    TeamMemberId = teamMemberId;
    Position = position;
  }

  internal void ChangePosition(MatchPosition position)
  {
    Position = position;
  }
}
