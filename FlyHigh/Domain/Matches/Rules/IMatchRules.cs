using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches.Rules;

public interface IMatchRules
{
  bool IsMatchFinished(
    IReadOnlyCollection<MatchSet> sets);

  bool IsExtraSetRequired(
    IReadOnlyCollection<MatchSet> sets);


}
