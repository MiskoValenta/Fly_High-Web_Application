using Application.DTOs.Teams;
using Application.Interfaces.Teams;
using Domain.Entities.Teams;
using Domain.Entities.Teams.TeamEnums;
using Domain.Repositories.Teams;
using Domain.Repositories.Users;
using Domain.Value_Objects.Teams;
using Domain.Value_Objects.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Teams;

public class TeamService : ITeamService
{
  private readonly ITeamRepository _teamRepository;
  private readonly IUserRepository _userRepository;
  private readonly ITeamAuthorizationService _auth;

  public TeamService(
      ITeamRepository teamRepository,
      IUserRepository userRepository,
      ITeamAuthorizationService auth)
  {
    _teamRepository = teamRepository;
    _userRepository = userRepository;
    _auth = auth;
  }

  public async Task<Guid> CreateTeamAsync(CreateTeamDto dto, Guid currentUserId)
  {
    var ownerId = new UserId(currentUserId);
    var team = Team.Create(ownerId, dto.TeamName, dto.ShortName, dto.Description);

    await _teamRepository.AddAsync(team);
    await _teamRepository.SaveChangesAsync();

    return team.Id.Value;
  }

  public async Task<List<TeamResponseDto>> GetUserTeamsAsync(Guid currentUserId, CancellationToken cancellationToken = default)
  {
    var userId = new UserId(currentUserId);
    var teams = await _teamRepository.GetUserTeamsAsync(userId, cancellationToken);

    return teams.Select(t => new TeamResponseDto(
        t.Id.Value,
        t.TeamName,
        t.ShortName,
        t.GetRole(userId)?.ToString() ?? "Unknown",
        t.GetMember(userId).Status.ToString()
    )).ToList();
  }

  public async Task<TeamDetailDto> GetTeamByIdAsync(Guid teamId, Guid currentUserId)
  {
    var team = await _teamRepository.GetByIdAsync(new TeamId(teamId));
    if (team is null)
      throw new KeyNotFoundException("Tým nebyl nalezen.");

    var currentUserRole = team.GetRole(new UserId(currentUserId))?.ToString() ?? "Unknown";

    var memberDtos = new List<TeamMemberDto>();
    foreach (var member in team.Members)
    {
      var user = await _userRepository.GetByIdAsync(member.UserId);
      if (user != null)
      {
        memberDtos.Add(new TeamMemberDto(
            user.Id.Value,
            user.Email,
            member.Role.ToString(),
            member.IsActive
        ));
      }
    }

    return new TeamDetailDto(
        team.Id.Value,
        team.TeamName,
        team.ShortName,
        team.Description,
        currentUserRole,
        memberDtos
    );
  }

  public async Task DeleteTeamAsync(Guid teamId, Guid actorId)
  {
    bool isAuthorized = await _auth.HasRoleInTeamAsync(actorId, teamId, TeamRole.Owner);
    if (!isAuthorized)
      throw new UnauthorizedAccessException("Pouze majitel může smazat tým.");

    var team = await _teamRepository.GetByIdAsync(new TeamId(teamId));
    if (team is null)
      throw new KeyNotFoundException("Tým nebyl nalezen.");

    team.DeleteTeam();
    await _teamRepository.SaveChangesAsync();
  }

  public async Task InviteMemberAsync(Guid teamId, Guid actorId, AddMemberDto dto)
  {
    bool isAuthorized = await _auth.HasRoleInTeamAsync(actorId, teamId, TeamRole.Owner, TeamRole.Coach);
    if (!isAuthorized)
      throw new UnauthorizedAccessException("Nemáš oprávnění přidávat/zvát členy do tohoto týmu.");

    var team = await _teamRepository.GetByIdAsync(new TeamId(teamId));
    if (team is null)
      throw new KeyNotFoundException("Tým nebyl nalezen.");

    var targetUser = await _userRepository.GetByIdAsync(new UserId(dto.TargetId));
    if (targetUser is null)
      throw new KeyNotFoundException("Uživatel, kterého se snažíš přidat, neexistuje.");

    team.InviteMember(targetUser.Id, dto.SetRole);

    await _teamRepository.SaveChangesAsync();
  }

  public async Task AcceptInvitationAsync(Guid teamId, Guid currentUserId)
  {
    var team = await _teamRepository.GetByIdAsync(new TeamId(teamId));
    if (team is null)
      throw new KeyNotFoundException("Tým nebyl nalezen.");

    team.AcceptInvitation(new UserId(currentUserId));
    await _teamRepository.SaveChangesAsync();
  }

  public async Task DeclineInvitationAsync(Guid teamId, Guid currentUserId)
  {
    var team = await _teamRepository.GetByIdAsync(new TeamId(teamId));
    if (team is null)
      throw new KeyNotFoundException("Tým nebyl nalezen.");

    team.DeclineInvitation(new UserId(currentUserId));
    await _teamRepository.SaveChangesAsync();
  }

  public async Task<List<PendingInvitationDto>> GetPendingInvitationsAsync(Guid currentUserId, CancellationToken cancellationToken = default)
  {
    var userId = new UserId(currentUserId);
    var allTeams = await _teamRepository.GetUserTeamsAsync(userId, cancellationToken);

    return allTeams
        .Select(t => new { Team = t, Member = t.Members.FirstOrDefault(m => m.UserId == userId) })
        .Where(x => x.Member != null && x.Member.Status == TeamMemberStatus.Pending)
        .Select(x => new PendingInvitationDto(
            x.Team.Id.Value,
            x.Team.TeamName,
            x.Member!.Role.ToString(),
            x.Member.InvitedAt
        )).ToList();
  }

  public async Task RemoveMemberAsync(Guid teamId, Guid actorId, Guid targetMemberId)
  {
    bool isAuthorized = await _auth.HasRoleInTeamAsync(actorId, teamId, TeamRole.Owner, TeamRole.Coach);
    if (!isAuthorized)
      throw new UnauthorizedAccessException("Nemáš oprávnění odebírat členy z tohoto týmu.");

    var team = await _teamRepository.GetByIdAsync(new TeamId(teamId));
    if (team is null)
      throw new KeyNotFoundException("Tým nebyl nalezen.");

    team.RemoveMember(new UserId(targetMemberId));

    await _teamRepository.SaveChangesAsync();
  }

  public async Task PromoteMemberAsync(Guid teamId, Guid actorId, Guid targetMemberId, ChangeRoleDto dto)
  {
    bool isAuthorized = await _auth.HasRoleInTeamAsync(actorId, teamId, TeamRole.Owner);
    if (!isAuthorized)
      throw new UnauthorizedAccessException("Pouze majitel týmu může měnit role členům.");

    var team = await _teamRepository.GetByIdAsync(new TeamId(teamId));
    if (team is null)
      throw new KeyNotFoundException("Tým nebyl nalezen.");

    team.ChangeMemberRole(new UserId(targetMemberId), dto.NewRole);

    await _teamRepository.SaveChangesAsync();
  }
}
