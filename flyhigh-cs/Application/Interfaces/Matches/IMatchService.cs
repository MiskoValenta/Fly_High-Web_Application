using Application.DTOs.Matches;
using Domain.Entities.Matches.MatchEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Matches;

public interface IMatchService
{
  Task<Guid> ProposeMatchAsync(CreateMatchDto dto, CancellationToken cancellationToken = default);
  Task AcceptMatchAsync(Guid matchId, CancellationToken cancellationToken = default);
  Task RejectMatchAsync(Guid matchId, CancellationToken cancellationToken = default);
  Task SetRefereeAsync(Guid matchId, Guid refereeId, CancellationToken cancellationToken = default);
  Task AddPlayerToRosterAsync(Guid matchId, AddRosterEntryDto dto, CancellationToken cancellationToken = default);
  Task StartMatchAsync(Guid matchId, CancellationToken cancellationToken = default);
  Task AssignPlayerPositionAsync(Guid matchId, AssignPositionDto dto, CancellationToken cancellationToken = default);
  Task AddPointAsync(Guid matchId, SetSide side, CancellationToken cancellationToken = default);
  Task CancelMatchAsync(Guid matchId, CancelMatchDto dto, CancellationToken cancellationToken = default);
}
