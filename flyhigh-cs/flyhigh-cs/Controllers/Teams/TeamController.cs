using Application.DTOs.Teams;
using Application.Services.Teams;
using Domain.Entities.Users;
using Domain.Repositories.Teams;
using Domain.Value_Objects.Teams;
using Domain.Value_Objects.Users;
using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers.Teams;

[ApiController]
[Route("api/teams")]
public class TeamController : ControllerBase
{
  private readonly ITeamService _teamService;

  public TeamController(ITeamService teamService)
  {
    _teamService = teamService;
  }

  private Guid GetCurrentUserId()
  {
    var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                   ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

    if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
    {
      throw new UnauthorizedAccessException("Token neobsahuje platné ID uživatele.");
    }
    return currentUserId;
  }

  [HttpPost]
  public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto dto)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      var newTeamId = await _teamService.CreateTeamAsync(dto, currentUserId);
      return Ok(new { TeamId = newTeamId });
    }
    catch (UnauthorizedAccessException ex) { return Unauthorized(ex.Message); }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }

  [HttpGet]
  public async Task<IActionResult> GetMyTeams(CancellationToken cancellationToken)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      var teams = await _teamService.GetUserTeamsAsync(currentUserId, cancellationToken);
      return Ok(teams);
    }
    catch (UnauthorizedAccessException ex) { return Unauthorized(ex.Message); }
  }

  [HttpGet("{teamId}")]
  public async Task<IActionResult> GetTeam([FromRoute] Guid teamId)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      var teamDetail = await _teamService.GetTeamByIdAsync(teamId, currentUserId);
      return Ok(teamDetail);
    }
    catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
    catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }

  [HttpDelete("{teamId}")]
  public async Task<IActionResult> DeleteTeam([FromRoute] Guid teamId)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      await _teamService.DeleteTeamAsync(teamId, currentUserId);
      return NoContent();
    }
    catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
    catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }


  [HttpPost("{teamId}/invites")]
  public async Task<IActionResult> InviteMember([FromRoute] Guid teamId, [FromBody] AddMemberDto dto)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      await _teamService.InviteMemberAsync(teamId, currentUserId, dto);
      return Ok(new { Message = "Pozvánka byla úspěšně odeslána." });
    }
    catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
    catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }

  [HttpPatch("{teamId}/invites/accept")]
  public async Task<IActionResult> AcceptInvitation([FromRoute] Guid teamId)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      await _teamService.AcceptInvitationAsync(teamId, currentUserId);
      return Ok(new { Message = "Pozvánka byla přijata." });
    }
    catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }

  [HttpPatch("{teamId}/invites/decline")]
  public async Task<IActionResult> DeclineInvitation([FromRoute] Guid teamId)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      await _teamService.DeclineInvitationAsync(teamId, currentUserId);
      return Ok(new { Message = "Pozvánka byla odmítnuta." });
    }
    catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }

  [HttpGet("invites/pending")]
  public async Task<IActionResult> GetPendingInvitations(CancellationToken cancellationToken)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      var invites = await _teamService.GetPendingInvitationsAsync(currentUserId, cancellationToken);
      return Ok(invites);
    }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }

  [HttpDelete("{teamId}/members/{memberId}")]
  public async Task<IActionResult> RemoveMember([FromRoute] Guid teamId, [FromRoute] Guid memberId)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      await _teamService.RemoveMemberAsync(teamId, currentUserId, memberId);
      return NoContent();
    }
    catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
    catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }

  [HttpPut("{teamId}/members/{memberId}/role")]
  public async Task<IActionResult> ChangeRole([FromRoute] Guid teamId, [FromRoute] Guid memberId, [FromBody] ChangeRoleDto dto)
  {
    try
    {
      var currentUserId = GetCurrentUserId();
      await _teamService.PromoteMemberAsync(teamId, currentUserId, memberId, dto);
      return Ok(new { Message = "Role byla úspěšně změněna." });
    }
    catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
    catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    catch (Exception ex) { return BadRequest(ex.Message); }
  }
}

