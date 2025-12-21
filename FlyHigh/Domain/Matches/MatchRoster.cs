using Domain.Matches.MatchEnums;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public class MatchRoster
{
  private readonly List<MatchRosterEntry> _entries = new();

  public IReadOnlyCollection<MatchRosterEntry> Entries => _entries.AsReadOnly();

  private MatchRoster() { }

  internal static MatchRoster Create()
    => new MatchRoster();

  internal void AddPlayer(
    TeamMemberId teamMemberId,
    MatchPosition position)
  {
    if (_entries.Any(e => e.TeamMemberId == teamMemberId))
      throw new InvalidOperationException("Player already in match roster");

    _entries.Add(new MatchRosterEntry(teamMemberId, position));
  }

  internal void ChangePosition(
    TeamMemberId teamMemberId,
    MatchPosition newPosition)
  {
    var entry = _entries.SingleOrDefault(e => e.TeamMemberId == teamMemberId)
      ?? throw new InvalidOperationException("Player not in roster");

    entry.ChangePosition(newPosition);
  }
}
