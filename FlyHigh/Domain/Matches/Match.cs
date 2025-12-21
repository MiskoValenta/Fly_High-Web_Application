using Domain.Matches.MatchEnums;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public class Match
{
  private readonly List<MatchSet> _sets = new();

  public IReadOnlyCollection<MatchSet> Sets => _sets.AsReadOnly();

  public MatchId Id { get; private set; }
  public TeamId HomeTeamId { get; private set; }
  public TeamId AwayTeamId { get; private set; }
  public MatchRoster Roster { get; private set; }
  public DateTime ScheduledAt { get; private set; }
  public string RefereeName { get; private set; }
  public string? Notes { get; private set; }
  public MatchStatus Status { get; private set; }
  public DateTime CreatedAt { get; private set; }

  private Match() { }

  private Match(
    MatchId id,
    TeamId homeTeamId,
    TeamId awayTeamId,
    DateTime scheduledAt,
    string refereeName)
  {
    Id = id;
    HomeTeamId = homeTeamId;
    AwayTeamId = awayTeamId;
    ScheduledAt = scheduledAt;
    RefereeName = refereeName;
    Status = MatchStatus.Scheduled;
    CreatedAt = DateTime.UtcNow;
    Roster = MatchRoster.Create();
  }

  public static Match Create(
    TeamId homeTeamId,
    TeamId awayTeamId,
    DateTime scheduledAt,
    string refereeName)
  {
    if (homeTeamId == awayTeamId)
      throw new InvalidOperationException("Home and Away teams must be different");

    return new Match(
      MatchId.New(),
      homeTeamId,
      awayTeamId,
      scheduledAt,
      refereeName);
  }

  public void StartMatch()
  {
    if (Status != MatchStatus.Scheduled)
      throw new InvalidOperationException("Match already started");

    _sets.Add(MatchSet.Create(1, SetType.Normal));
    Status = MatchStatus.InProgress;
  }

  public void StartNextSet()
  {
    if (Status != MatchStatus.InProgress)
      throw new InvalidOperationException("Match is not in progress");

    var lastSet = _sets.Last();

    if (!lastSet.IsFinished)
      throw new InvalidOperationException("Current set is not finished");

    int homeWins = _sets.Count(s => s.Winner == SetWinner.Home);
    int awayWins = _sets.Count(s => s.Winner == SetWinner.Away);

    if (homeWins == 3 || awayWins == 3)
    {
      Status = MatchStatus.Finished;
      return;
    }

    bool isExtra = homeWins == 2 && awayWins == 2;

    _sets.Add(
      MatchSet.Create(
        _sets.Count + 1,
        isExtra ? SetType.Extra : SetType.Normal
      )
    );
  }

  public void AddToRoster(TeamMemberId teamMemberId, MatchPosition position)
  {
    Roster.AddPlayer(teamMemberId, position);
  }
}