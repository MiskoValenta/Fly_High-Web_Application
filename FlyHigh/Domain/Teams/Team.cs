using Domain.Users;
using Domain.TeamMembers;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using Domain.Teams.TeamEnums;

namespace Domain.Teams;

public class Team : AuditableEntity<TeamId>
{
  private readonly List<TeamMember> _members = new();
  public IReadOnlyCollection<TeamMember> Members => _members.AsReadOnly();

  public string TeamName { get; private set; } = string.Empty;
  public string ShortName { get; private set; } = string.Empty;
  public string? Description { get; private set; } = string.Empty;

  private Team() { }

  private Team(
      TeamId id,
      string teamName,
      string shortName,
      string? description) : base(id)
  {
    TeamName = teamName;
    ShortName = shortName;
    Description = description;
  }

  public static Team Create(
      string teamName,
      string shortName,
      string? description = null)
  {
    if (string.IsNullOrWhiteSpace(teamName))
      throw new ArgumentException("Team name cannot be empty.", nameof(teamName));

    if (string.IsNullOrWhiteSpace(shortName))
      throw new ArgumentException("Shortname for Team cannot be empty.", nameof(shortName));

    return new Team(
        TeamId.New(),
        teamName,
        shortName,
        description);
  }

  public void AddMember(UserId userId, TeamRole role)
  {
    if (_members.Any(m => m.UserId == userId))
      throw new InvalidOperationException($"User {userId} is already a member of this team.");

    if (role == TeamRole.Captain && _members.Any(m => m.Role == TeamRole.Captain))
      throw new InvalidOperationException("Team already has a captain. Demote the current captain first.");

    var member = new TeamMember(
        TeamMemberId.New(),
        userId,
        this.Id,
        role,
        DateTime.UtcNow
    );

    _members.Add(member);

    MarkAsModified();
  }

  public void RemoveMember(UserId userId)
  {
    var member = _members.FirstOrDefault(m => m.UserId == userId);
    if (member == null)
      throw new InvalidOperationException("User is not in this team.");

    if (member.Role == TeamRole.Captain && _members.Count > 1)
      throw new InvalidOperationException("Cannot remove captain unless they are the last member. Assign a new captain first.");

    _members.Remove(member);
    MarkAsModified();
  }

  public void UpdateDescription(string newDescription)
  {
    Description = newDescription;
    MarkAsModified();
  }

  public void PromoteToCaptain(UserId userId)
  {
    var newCaptain = _members.FirstOrDefault(m => m.UserId == userId);
    if (newCaptain == null)
      throw new InvalidOperationException("User is not in this team.");

    var oldCaptain = _members.FirstOrDefault(m => m.Role == TeamRole.Captain);

    if (oldCaptain != null)
    {
      oldCaptain.ChangeRole(TeamRole.Player);
    }

    newCaptain.ChangeRole(TeamRole.Captain);
    MarkAsModified();
  }
}
