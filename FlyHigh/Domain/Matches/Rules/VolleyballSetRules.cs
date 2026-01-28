using Domain.Matches.MatchEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches.Rules;

public class VolleyballSetRules : ISetRules
{
  private const int NormalSetTargetScore = 25;
  private const int ExtraSetTargetScore = 15;
  private const int MinimumDifference = 2;

  public bool IsWinningScore(
    int score,
    int opponentScore,
    SetType setType)
  {
    int targetScore;

    if (setType == SetType.Extra)
      targetScore = ExtraSetTargetScore;
    else
      targetScore = NormalSetTargetScore;

    if (score < targetScore)
      return false;

    if (score - opponentScore < MinimumDifference)
      return false;

    return true;
  }
}
