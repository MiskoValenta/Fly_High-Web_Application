using Domain.Common;
using Domain.Matches.MatchEnums;
using Domain.Matches.Rules;
using Domain.TeamMembers;
using Domain.Teams;

namespace Domain.Matches;

public class Match : AuditableEntity<MatchId>
{
  private readonly List<MatchSet> _sets = new();
  private readonly List<MatchRosterEntry> _roster = new();

  private readonly ISetRules _setRules;
  private readonly IMatchRules _matchRules;

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
    string? notes,
    ISetRules setRules,
    IMatchRules matchRules) : base(id)
  {
    if (setRules == null)
      throw new ArgumentNullException(nameof(setRules));

    if (matchRules == null)
      throw new ArgumentNullException(nameof(matchRules));

    HomeTeamId = homeTeamId;
    AwayTeamId = awayTeamId;
    ScheduledAt = scheduledAt;
    RefereeName = refereeName;
    Notes = notes;

    _setRules = setRules;
    _matchRules = matchRules;

    Status = MatchStatus.Scheduled;
  }

  public static Match Create(
    TeamId homeTeamId,
    TeamId awayTeamId,
    DateTime scheduledAt,
    string refereeName,
    string? notes,
    ISetRules setRules,
    IMatchRules matchRules)
  {
    if (homeTeamId == awayTeamId)
      throw new InvalidOperationException("Home and Away teams must be different");

    return new Match(
      MatchId.New(),
      homeTeamId,
      awayTeamId,
      scheduledAt,
      refereeName,
      notes,
      setRules,
      matchRules);
  }

  public void StartMatch()
  {
    if (Status != MatchStatus.Scheduled)
      throw new InvalidOperationException("Match already started");

    ValidateRoster();

    _sets.Add(
      MatchSet.Create(1, SetType.Normal, _setRules)
    );

    Status = MatchStatus.InProgress;
    MarkAsModified();
  }

  public void AddPoint(SetSide side)
  {
    if (Status != MatchStatus.InProgress)
      throw new InvalidOperationException("Match is not in progress");

    var currentSet = _sets.Last();
    currentSet.AddPoint(side);

    if (!currentSet.IsFinished)
      return;

    HandleFinishedSet();
    MarkAsModified();
  }

  private void HandleFinishedSet()
  {
    if (_matchRules.IsMatchFinished(_sets))
    {
      Status = MatchStatus.Finished;
      return;
    }

    bool isExtraSet = _matchRules.IsExtraSetRequired(_sets);

    _sets.Add(
      MatchSet.Create(
        _sets.Count + 1,
        isExtraSet ? SetType.Extra : SetType.Normal,
        _setRules
      )
    );
  }

  public void AddToRoster(
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

    _roster.Add(
      MatchRosterEntry.Create(
        Id,
        teamMemberId,
        teamId,
        position,
        jerseyNumber
      )
    );

    MarkAsModified();
  }

  private void ValidateRoster()
  {
    int homeCount = _roster.Count(x => x.TeamId == HomeTeamId);
    int awayCount = _roster.Count(x => x.TeamId == AwayTeamId);

    if (homeCount < 6 || awayCount < 6)
      throw new InvalidOperationException("Both teams must have at least 6 players.");
  }

  public void SetCaptain(TeamId teamId, TeamMemberId teamMemberId)
  {
    if (Status != MatchStatus.Scheduled)
      throw new InvalidOperationException("Cannot change captain after match start.");

    var player = _roster.SingleOrDefault(x =>
    x.TeamId == teamId &&
    x.TeamMemberId == teamMemberId);

    if (player == null)
      throw new InvalidOperationException("Player not found in roster.");

    foreach (var entry in _roster.Where(x => x.TeamId == teamId && x.IsCaptain))
      entry.RemoveCaptain();

    player.AssignCaptian();
    MarkAsModified();
  }

  public void ChangePlayerPosition(
    TeamMemberId teamMemberId,
    MatchPosition newPosition)
  {
    if (Status != MatchStatus.Scheduled)
      throw new InvalidOperationException("Cannot change position after match start.");

    var player = _roster.SingleOrDefault(x => x.TeamMemberId == teamMemberId);

    if (player == null)
      throw new InvalidOperationException("Player not found in roster.");

    player.UpdatePosition(newPosition);
    MarkAsModified();
  }

  public void SetStartingLineup(
  TeamId teamId,
  IDictionary<TeamMemberId, CourtPosition> lineup)
  {
    if (Status != MatchStatus.Scheduled)
      throw new InvalidOperationException("Cannot set lineup after match start.");

    if (lineup.Count != 6)
      throw new InvalidOperationException("Starting lineup must have exactly 6 players.");

    var usedPositions = lineup.Values.Distinct().Count();
    if (usedPositions != 6)
      throw new InvalidOperationException("Each court position must be unique.");

    var teamPlayers = _roster.Where(x => x.TeamId == teamId).ToList();

    foreach (var entry in teamPlayers)
    {
      entry.RemoveFromCourt();
      entry.ClearCourtPosition();
    }

    foreach (var pair in lineup)
    {
      var entry = teamPlayers.SingleOrDefault(x => x.TeamMemberId == pair.Key);
      if (entry == null)
        throw new InvalidOperationException("Player not in roster.");

      entry.PutOnCourt();
      entry.AssignCourtPosition(pair.Value);
    }

    MarkAsModified();
  }

  public void SubstitutePlayer(
    TeamMemberId playerOut,
    TeamMemberId playerIn)
  {
    if (Status != MatchStatus.InProgress)
      throw new InvalidOperationException("Substitutions allowed only during match.");

    var outEntry = _roster.SingleOrDefault(x => x.TeamMemberId == playerOut);
    var inEntry = _roster.SingleOrDefault(x => x.TeamMemberId == playerIn);

    if (outEntry == null || inEntry == null)
      throw new InvalidOperationException("Player not found");

    if (outEntry.TeamId != inEntry.TeamId)
      throw new InvalidOperationException("Players must be from the same team.");

    if (!outEntry.IsOnCourt || inEntry.IsOnCourt)
      throw new InvalidOperationException("Invalid substitution.");

    outEntry.RemoveFromCourt();
    inEntry.PutOnCourt();

    MarkAsModified();
  }

  public void RotateTeam(TeamId teamId)
  {
    if (Status != MatchStatus.InProgress)
      throw new InvalidOperationException("Rotation allowed only during match.");

    var onCourt = _roster
      .Where(x => x.TeamId == teamId && x.IsOnCourt)
      .ToList();

    if (onCourt.Count != 6)
      throw new InvalidOperationException("Rotation requires exactly 6 players on court.");

    var positionMap = onCourt.ToDictionary(
      x => x.CourtPosition!.Value,
      x => x
    );

    foreach (var entry in onCourt)
    {
      var current = entry.CourtPosition!.Value;
      var next = GetNextPosition(current);
      entry.AssignCourtPosition(next);
    }

    MarkAsModified();
  }

  private CourtPosition GetNextPosition(CourtPosition position)
  {
    if (position == CourtPosition.Position1)
      return CourtPosition.Position6;

    return (CourtPosition)((int)position - 1);
  }
}