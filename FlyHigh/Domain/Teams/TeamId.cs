using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Teams;

public readonly record struct TeamId(Guid Value)
{
  public static TeamId New() => new(Guid.NewGuid());
}
