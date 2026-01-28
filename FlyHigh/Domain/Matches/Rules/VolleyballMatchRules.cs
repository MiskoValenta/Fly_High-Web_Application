using Domain.Matches.MatchEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches.Rules;

public class VolleyballMatchRules : IMatchRules
{
  private const int SetsToWin = 3;
  private const int DrawSets = 2;

  public bool IsMatchFinished(IReadOnlyCollection<MatchSet> sets)
  {
    int homeWins = sets.Count(s => s.Winner == SetWinner.Home);
    int awayWins = sets.Count(s => s.Winner == SetWinner.Away);

    if (homeWins == SetsToWin)
      return true;

    if (awayWins == SetsToWin)
      return true;

    return false;
  }

  public bool IsExtraSetRequired(IReadOnlyCollection<MatchSet> sets)
  {
    int homeWins = sets.Count(s => s.Winner == SetWinner.Home);
    int awayWins = sets.Count(s => s.Winner == SetWinner.Away);

    if (homeWins == DrawSets && awayWins == DrawSets)
      return true;

    return false;
  }
}
