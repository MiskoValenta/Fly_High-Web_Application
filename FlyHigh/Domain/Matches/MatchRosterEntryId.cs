using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public readonly record struct MatchRosterEntryId(Guid Value)
{
  public static MatchRosterEntryId New() => new(Guid.NewGuid());
}
