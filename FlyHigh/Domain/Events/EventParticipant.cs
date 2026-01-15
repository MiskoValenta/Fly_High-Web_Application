using Domain.Events.EventEnums;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events;

public class EventParticipant
{
  public UserId UserId { get; private set; }
  public EventResponse Response { get; private set; }
  public DateTime AddedAt { get; private set; }
  public DateTime? RespondedAt { get; private set; }

  private EventParticipant() { }

  internal EventParticipant(UserId userId)
  {
    UserId = userId;
    Response = EventResponse.Ignored;
    AddedAt = DateTime.UtcNow;
  }

  internal void Respond(EventResponse response)
  {
    if (response == EventResponse.Ignored)
      throw new InvalidOperationException("Ignored participant response");

    Response = response;
    RespondedAt = DateTime.UtcNow;
  }
}
