using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public readonly record struct MatchSetId(Guid Value)
{
  public static MatchSetId New() => new(Guid.NewGuid());
}
