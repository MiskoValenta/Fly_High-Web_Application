using Domain.Common;
using Domain.Matches.MatchEnums;
using Domain.TeamMembers;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public class Match : AuditableEntity<MatchId>
{
  private readonly List<MatchSet> _sets = new();
  private readonly List<MatchRosterEntry> _roster = new();

  public TeamId HomeTeamId { get; private set; }
  public TeamId AwayTeamId { get; private set; }

  public IReadOnlyCollection<MatchSet> Sets => _sets.AsReadOnly();
  public IReadOnlyCollection<MatchRosterEntry> Roster => _roster.AsReadOnly();

  public DateTime ScheduledAt { get; private set; }
  public string RefereeName { get; private set; }
  public string? Notes { get; private set; }
  public MatchStatus Status { get; private set; }

  private Match() { }

  private Match(
    MatchId id,
    TeamId homeTeamId,
    TeamId awayTeamId,
    DateTime scheduledAt,
    string refereeName,
    string? notes) : base(id)
  {
    HomeTeamId = homeTeamId;
    AwayTeamId = awayTeamId;
    ScheduledAt = scheduledAt;
    RefereeName = refereeName;
    Status = MatchStatus.Scheduled;
    Notes = notes;
  }

  public static Match Create(
    TeamId homeTeamId,
    TeamId awayTeamId,
    DateTime scheduledAt,
    string refereeName,
    string? notes)
  {
    if (homeTeamId == awayTeamId)
      throw new InvalidOperationException("Home and Away teams must be different");

    return new Match(
        MatchId.New(),
        homeTeamId,
        awayTeamId,
        scheduledAt,
        refereeName,
        notes);
  }

  public void StartMatch()
  {
    if (Status != MatchStatus.Scheduled)
      throw new InvalidOperationException("Match already started");

    ValidateRoster(); 

    _sets.Add(MatchSet.Create(1, SetType.Normal));
    Status = MatchStatus.InProgress;

    MarkAsModified();
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
      MarkAsModified();
      return;
    }

    bool isExtra = homeWins == 2 && awayWins == 2;

    _sets.Add(
        MatchSet.Create(
            _sets.Count + 1,
            isExtra ? SetType.Extra : SetType.Normal
        )
    );

    MarkAsModified();
  }

  public void AddToRoster(
        MatchId matchId,
        TeamMemberId teamMemberId,
        TeamId teamId,
        MatchPosition position,
        int jerseyNumber)
  {
    if (Status != MatchStatus.Scheduled)
      throw new InvalidOperationException("Cannot add players after match started.");

    if (teamId != HomeTeamId && teamId != AwayTeamId)
      throw new InvalidOperationException("Team does not play in this match.");

    if (_roster.Any(x => x.TeamMemberId == teamMemberId))
      throw new InvalidOperationException("Player is already on the roster.");

    if (_roster.Any(x => x.TeamId == teamId && x.JerseyNumber == jerseyNumber))
      throw new InvalidOperationException($"Jersey number {jerseyNumber} is already taken in this team.");

    var entry = new MatchRosterEntry(
        MatchRosterEntryId.New(),
        matchId,
        teamMemberId,
        teamId,
        position,
        jerseyNumber
    );

    _roster.Add(entry);
    MarkAsModified();
  }

  private void ValidateRoster()
  {
    var homeCount = _roster.Count(x => x.TeamId == HomeTeamId);
    var awayCount = _roster.Count(x => x.TeamId == AwayTeamId);

    if (homeCount < 6 || awayCount < 6)
      throw new InvalidOperationException("Both teams must have at least 6 players.");
  }
}