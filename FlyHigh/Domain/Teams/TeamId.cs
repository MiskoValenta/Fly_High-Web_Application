using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Teams;

public record TeamId(Guid Value) : StronglyTypedId(Value)
{
  public static TeamId New() => new(Guid.NewGuid());
}
