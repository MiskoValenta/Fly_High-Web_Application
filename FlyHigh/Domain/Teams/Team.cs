using Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Teams;

public class Team
{
  private readonly HashSet<TeamMember> _members = new();

  public TeamId Id { get; private set; }
  public string TeamName { get; private set; } = string.Empty;
  public string ShortName { get; private set; } = string.Empty;
  public string? Description { get; private set; } = string.Empty;
  public DateTime CreatedAt { get; private set; }

  public IReadOnlyCollection<TeamMember> Members => _members.ToList().AsReadOnly();

  private Team() { }

  private Team(
    TeamId id,
    string teamName,
    string shortName,
    string? description)
  {
    Id = id;
    TeamName = teamName;
    ShortName = shortName;
    Description = description;
    CreatedAt = DateTime.UtcNow;
  }

  public static Team Create(
    string teamName,
    string shortName,
    string? description = null)
  {
    if (string.IsNullOrWhiteSpace(teamName))
      throw new ArgumentException("Team name cannot be empty");

    if (string.IsNullOrWhiteSpace(shortName))
      throw new ArgumentException("Shortname for Team cannot be empty");

    return new Team(
      TeamId.New(),
      teamName,
      shortName,
      description);
  }

  public void AddMember(UserId userId, TeamRole role)
  {
    if (_members.Any(m => m.UserId == userId))
      throw new InvalidOperationException("User is already a member of this team");

    if (role == TeamRole.Captain && _members.Any(m => m.Role == TeamRole.Captain))
      throw new InvalidOperationException("Team already has a captain");

    _members.Add(new TeamMember(userId, role));
  }

  public void RemoveMember(UserId userId)
  {
    var member = _members.FirstOrDefault(m => m.UserId == userId);
    if (member == null)
      throw new InvalidOperationException("User is not in this team");

    _members.Remove(member);
  }
}
