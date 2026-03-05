using Application.Common.Interfaces;
using Application.DTOs.Matches;
using Application.Interfaces.Matches;
using Domain.Entities.Matches;
using Domain.Entities.Matches.Exceptions;
using Domain.Entities.Matches.MatchEnums;
using Domain.Entities.Matches.Rules;
using Domain.Repositories.Matches;
using Domain.Value_Objects.Matches;
using Domain.Value_Objects.Teams;
using Domain.Value_Objects.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Matches;

public class MatchService : IMatchService
{
  private readonly IMatchRepository _matchRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ISetRules _setRules;
  private readonly IMatchRules _matchRules;

  public MatchService(
      IMatchRepository matchRepository,
      IUnitOfWork unitOfWork,
      ISetRules setRules,
      IMatchRules matchRules)
  {
    _matchRepository = matchRepository;
    _unitOfWork = unitOfWork;
    _setRules = setRules;
    _matchRules = matchRules;
  }

  public async Task<Guid> ProposeMatchAsync(CreateMatchDto dto, CancellationToken cancellationToken = default)
  {
    var match = Match.CreateInvitation(
        new TeamId(dto.HomeTeamId),
        new TeamId(dto.AwayTeamId),
        dto.ScheduledAt,
        dto.Location,
        _setRules,
        _matchRules
    );

    await _matchRepository.AddAsync(match, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return match.Id.Value;
  }

  public async Task AcceptMatchAsync(Guid matchId, CancellationToken cancellationToken = default)
  {
    var match = await GetMatchOrThrowAsync(matchId, cancellationToken);
    match.AcceptInvitation();
    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }

  public async Task RejectMatchAsync(Guid matchId, CancellationToken cancellationToken = default)
  {
    var match = await GetMatchOrThrowAsync(matchId, cancellationToken);
    match.RejectInvitation();
    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }

  public async Task SetRefereeAsync(Guid matchId, Guid refereeId, CancellationToken cancellationToken = default)
  {
    var match = await GetMatchOrThrowAsync(matchId, cancellationToken);
    match.SetReferee(new UserId(refereeId));
    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }

  public async Task AddPlayerToRosterAsync(Guid matchId, AddRosterEntryDto dto, CancellationToken cancellationToken = default)
  {
    var match = await GetMatchOrThrowAsync(matchId, cancellationToken);
    match.AddToRoster(new TeamMemberId(dto.TeamMemberId), new TeamId(dto.TeamId), dto.JerseyNumber);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }

  public async Task StartMatchAsync(Guid matchId, CancellationToken cancellationToken = default)
  {
    var match = await GetMatchOrThrowAsync(matchId, cancellationToken);
    match.StartMatch();
    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }

  public async Task AssignPlayerPositionAsync(Guid matchId, AssignPositionDto dto, CancellationToken cancellationToken = default)
  {
    var match = await GetMatchOrThrowAsync(matchId, cancellationToken);
    match.AssignPlayerPositionForSet(dto.SetNumber, new TeamMemberId(dto.TeamMemberId), dto.Position);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }

  public async Task AddPointAsync(Guid matchId, SetSide side, CancellationToken cancellationToken = default)
  {
    var match = await GetMatchOrThrowAsync(matchId, cancellationToken);
    match.AddPoint(side);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }

  public async Task CancelMatchAsync(Guid matchId, CancelMatchDto dto, CancellationToken cancellationToken = default)
  {
    var match = await GetMatchOrThrowAsync(matchId, cancellationToken);
    match.CancelMatch(dto.Reason);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }

  private async Task<Match> GetMatchOrThrowAsync(Guid matchId, CancellationToken cancellationToken)
  {
    var match = await _matchRepository.GetByIdAsync(new MatchId(matchId), cancellationToken);
    if (match == null)
      throw new MatchInvalidException("Match not found");
    return match;
  }
}
