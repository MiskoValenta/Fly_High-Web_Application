using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events;

public readonly record struct EventId(Guid Value)
{
  public static EventId New() => new(Guid.NewGuid());
}
