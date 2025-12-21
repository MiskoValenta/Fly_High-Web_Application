using Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Teams;

public class TeamMember
{
  public TeamMemberId Id { get; private set; }
  public UserId UserId { get; private set; }
  public TeamRole Role { get; private set; }
  public DateTime JoinedAt { get; private set; }

  private TeamMember() { }

  internal TeamMember(TeamMemberId id, UserId userId, TeamRole role)
  {
    Id = id;
    UserId = userId;
    Role = role;
    JoinedAt = DateTime.UtcNow;
  }

  internal void ChangeRole(TeamRole role)
  {
    Role = role;
  }
}
