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
  private const int MinJerseyNumber = 1;
  private const int MaxJerseyNumber = 99;

  public MatchId MatchId { get; private set; }
  public TeamMemberId TeamMemberId { get; private set; }
  public TeamId TeamId { get; private set; }
  public MatchPosition Position { get; private set; }
  public bool IsCaptain { get; private set; }
  public int JerseyNumber { get; private set; }
  public bool IsOnCourt { get; private set; }
  public CourtPosition? CourtPosition { get; private set; }

  internal MatchRosterEntry(
    MatchRosterEntryId id,
    MatchId matchId,
    TeamMemberId teamMemberId,
    TeamId teamId,
    MatchPosition position,
    int jerseyNumber,
    bool isCaptain = false) : base(id)
  {
    if (jerseyNumber < MinJerseyNumber || jerseyNumber > MaxJerseyNumber)
      throw new InvalidOperationException("Invalid jersey number.");

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
    if (Position == newPosition)
      return;

    Position = newPosition;
    MarkAsModified();
  }

  internal void AssignCaptian()
  {
    if (IsCaptain)
      return;

    IsCaptain = true;
    MarkAsModified();
  }

  internal void RemoveCaptain()
  {
    if (!IsCaptain)
      return;

    IsCaptain = false;
    MarkAsModified();
  }

  internal void PutOnCourt()
  {
    IsOnCourt = true;
  }

  internal void RemoveFromCourt()
  {
    IsOnCourt = false;
  }

  internal void AssignCourtPosition(CourtPosition position)
  {
    CourtPosition = position;
  }

  internal void ClearCourtPosition()
  {
    CourtPosition = null;
  }

}
