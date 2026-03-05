using Domain.Entities.Teams.TeamEnums;
using Domain.Value_Objects.Teams;
using Domain.Value_Objects.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Teams;

public record CreateTeamDto(string TeamName, string ShortName, string? Description = null);
public record AddMemberDto(Guid TargetId, TeamRole SetRole, TeamMemberStatus pedningStatus);
public record ChangeRoleDto(TeamRole NewRole);
public record TeamMemberDto(Guid Id, string Email, string Role, bool IsActive);
public record TeamDetailDto(Guid Id, string TeamName, string ShortName, string? Description, string MyRole, List<TeamMemberDto> Members);
public record PendingInvitationDto(Guid TeamId, string TeamName, string InvitingRole, DateTime? InvitedAt);




