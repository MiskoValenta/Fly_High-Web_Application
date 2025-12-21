using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Teams;

public readonly record struct TeamMemberId(Guid Value)
{
  public static TeamMemberId New() => new(Guid.NewGuid());
}
