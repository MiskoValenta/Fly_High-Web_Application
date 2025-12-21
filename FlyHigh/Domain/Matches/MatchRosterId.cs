using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public readonly record struct MatchRosterId(Guid Value)
{
  public static MatchRosterId New() => new(Guid.NewGuid());
}
