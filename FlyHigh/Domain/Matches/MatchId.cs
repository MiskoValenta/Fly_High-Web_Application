using Domain.Common;

namespace Domain.Matches;

public record MatchId(Guid Value) : StronglyTypedId(Value)
{
  public static MatchId New() => new(Guid.NewGuid());
}
