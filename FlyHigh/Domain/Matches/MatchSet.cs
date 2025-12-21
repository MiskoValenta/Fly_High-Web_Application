using Domain.Matches.MatchEnums;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches;

public class MatchSet
{
  public MatchSetId Id { get; private set; }
  public int SetNumber { get; private set; }
  public SetType Type { get; private set; }

  public int HomeScore { get; private set; }
  public int AwayScore { get; private set; }

  public bool IsFinished { get; private set; }
  public SetWinner Winner { get; private set; }

  private MatchSet() { }

  private MatchSet(int setNumber, SetType type)
  {
    Id = MatchSetId.New();
    SetNumber = setNumber;
    Type = type;
    HomeScore = 0;
    AwayScore = 0;
    IsFinished = false;
    Winner = SetWinner.None;
  }

  internal static MatchSet Create(int setNumber, SetType type)
  {
    return new MatchSet(setNumber, type);
  }
  
  public void ScorePointForHome()
  {
    EnsureNotFinished();
    HomeScore++;
    TryFinish();
  }

  public void ScorePointForAway()
  {
    EnsureNotFinished();
    AwayScore++;
    TryFinish();
  }

  private void EnsureNotFinished()
  {
    if (IsFinished)
      throw new InvalidOperationException("Set is already finished");
  }

  private void TryFinish()
  {
    if (!HasReachedWinningCondition())
      return;

    IsFinished = true;
    Winner = HomeScore > AwayScore
      ? SetWinner.Home
      : SetWinner.Away;
  }

  private bool HasReachedWinningCondition()
  {
    int target = Type == SetType.Extra ? 15 : 25;

    return
      (HomeScore >= target || AwayScore >= target) &&
      Math.Abs(HomeScore - AwayScore) >= 2;
  }
}
