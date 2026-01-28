using Domain.Matches.MatchEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches.Rules;

public interface ISetRules
{
  bool IsWinningScore(
    int score,
    int opponentScore,
    SetType setType);
}
