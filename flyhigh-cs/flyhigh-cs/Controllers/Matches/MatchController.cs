using Application.DTOs.Matches;
using Application.Interfaces.Matches;
using Domain.Entities.Matches.MatchEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Matches;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MatchController : ControllerBase
{
  private readonly IMatchService _matchService;

  public MatchController(IMatchService matchService)
  {
    _matchService = matchService;
  }

  [HttpPost("propose")]
  public async Task<IActionResult> ProposeMatch([FromBody] CreateMatchDto dto, CancellationToken ct)
  {
    var matchId = await _matchService.ProposeMatchAsync(dto, ct);
    return Ok(new { MatchId = matchId });
  }

  [HttpPost("{id}/accept")]
  public async Task<IActionResult> AcceptMatch(Guid id, CancellationToken ct)
  {
    await _matchService.AcceptMatchAsync(id, ct);
    return NoContent();
  }

  [HttpPost("{id}/reject")]
  public async Task<IActionResult> RejectMatch(Guid id, CancellationToken ct)
  {
    await _matchService.RejectMatchAsync(id, ct);
    return NoContent();
  }

  [HttpPost("{id}/referee/{refereeId}")]
  public async Task<IActionResult> SetReferee(Guid id, Guid refereeId, CancellationToken ct)
  {
    await _matchService.SetRefereeAsync(id, refereeId, ct);
    return NoContent();
  }

  [HttpPost("{id}/roster")]
  public async Task<IActionResult> AddToRoster(Guid id, [FromBody] AddRosterEntryDto dto, CancellationToken ct)
  {
    await _matchService.AddPlayerToRosterAsync(id, dto, ct);
    return NoContent();
  }

  [HttpPost("{id}/start")]
  public async Task<IActionResult> StartMatch(Guid id, CancellationToken ct)
  {
    await _matchService.StartMatchAsync(id, ct);
    return NoContent();
  }

  [HttpPost("{id}/positions")]
  public async Task<IActionResult> AssignPosition(Guid id, [FromBody] AssignPositionDto dto, CancellationToken ct)
  {
    await _matchService.AssignPlayerPositionAsync(id, dto, ct);
    return NoContent();
  }

  [HttpPost("{id}/point/{side}")]
  public async Task<IActionResult> AddPoint(Guid id, SetSide side, CancellationToken ct)
  {
    await _matchService.AddPointAsync(id, side, ct);
    return NoContent();
  }

  [HttpPost("{id}/cancel")]
  public async Task<IActionResult> CancelMatch(Guid id, [FromBody] CancelMatchDto dto, CancellationToken ct)
  {
    await _matchService.CancelMatchAsync(id, dto, ct);
    return NoContent();
  }
}
