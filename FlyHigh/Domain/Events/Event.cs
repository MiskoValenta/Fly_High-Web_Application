using Domain.Common;
using Domain.Events.EventEnums;
using Domain.Matches;
using Domain.Teams;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events;

public class Event : AuditableEntity<EventId>
{
  private readonly List<EventParticipant> _participants = new();

  public IReadOnlyCollection<EventParticipant> Participants => _participants.AsReadOnly();

  public TeamId TeamId { get; private set; }
  public MatchId? MatchId { get; private set; }
  public string Title { get; private set; } = string.Empty;
  public string? Description { get; private set; }
  public EventType Type { get; private set; }
  public UserId CreatedBy { get; private set; }
  public DateTime CreatedAt { get; private set; }

  private Event(
    EventId id,
    TeamId teamId,
    MatchId? matchId,
    string title,
    string? description,
    EventType type,
    UserId createdBy,
    DateTime createdAt) : base(id)
  {
    if (string.IsNullOrWhiteSpace(title))
      throw new ArgumentException("Event title is required");

    TeamId = teamId;
    MatchId = matchId;
    Title = title;
    Description = description;
    Type = type;
    CreatedBy = createdBy;
    CreatedAt = createdAt;
  }
  private Event() { }

  public static Event Create(
    TeamId teamId,
    string title,
    EventType type,
    UserId createdBy,
    string? description = null,
    MatchId? matchId = null)
  {
    return new Event(
      EventId.New(),
      teamId,
      matchId,
      title,
      description,
      type,
      createdBy,
      DateTime.UtcNow);
  }

  public void AddParticipant(UserId userId)
  {
    if (_participants.Any(p => p.UserId == userId))
      throw new InvalidOperationException("User is already a participant");

    _participants.Add(new EventParticipant(userId));
  }

  public void Respond(UserId userId, EventResponse response)
  {
    var participant = _participants
      .FirstOrDefault(p => p.UserId == userId)
      ?? throw new InvalidOperationException("Participant not found");

    participant.Respond(response);
  }

  public void RemoveParticipant(UserId userId)
  {
    var participant = _participants
      .FirstOrDefault(p => p.UserId == userId)
      ?? throw new InvalidOperationException("Participant not found");

    _participants.Remove(participant);
  }
}
