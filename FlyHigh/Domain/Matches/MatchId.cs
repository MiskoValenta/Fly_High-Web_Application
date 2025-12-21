using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public readonly record struct MatchId(Guid Value)
{
  public static MatchId New() => new(Guid.NewGuid());
}
