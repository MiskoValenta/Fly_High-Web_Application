using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public record MatchRosterEntryId(Guid Value) : StronglyTypedId(Value)
{
  public static MatchRosterEntryId New() => new(Guid.NewGuid());
}

