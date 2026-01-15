using Domain.Common;
using Domain.Matches;
using Domain.Teams;
using Domain.Teams.TeamEnums;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Domain.TeamMembers;

public class TeamMember : AuditableEntity<TeamMemberId>
{
  public UserId UserId { get; private set; }
  public TeamId TeamId { get; private set; }
  public TeamRole Role { get; private set; }
  public DateTime JoinedAt { get; private set; }

  internal TeamMember(
        TeamMemberId id,
        UserId userId,
        TeamId teamId,
        TeamRole role,
        DateTime joinedAt) : base(id)
  {
    UserId = userId;
    TeamId = teamId;
    Role = role;
    JoinedAt = joinedAt;
  }

  private TeamMember() { }

  public static TeamMember Create(
    UserId userId,
    TeamId teamId,
    TeamRole role)
  {
    return new TeamMember(
      TeamMemberId.New(),
      userId,
      teamId,
      role,
      DateTime.UtcNow);
  }

  internal void ChangeRole(TeamRole newRole)
  {
    Role = newRole;
  }
}
