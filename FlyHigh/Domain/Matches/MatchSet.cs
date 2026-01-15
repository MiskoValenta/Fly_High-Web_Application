using Domain.Common;
using Domain.Matches.MatchEnums;
using Domain.Teams;
using System;

namespace Domain.Matches
{
  public class MatchSet : AuditableEntity<MatchSetId>
  {
    public int SetNumber { get; private set; }
    public SetType Type { get; private set; }

    public int HomeScore { get; private set; }
    public int AwayScore { get; private set; }

    public bool IsFinished { get; private set; }
    public SetWinner Winner { get; private set; }

    private MatchSet() { }

    private MatchSet(MatchSetId id, int setNumber, SetType type) : base(id)
    {
      SetNumber = setNumber;
      Type = type;
      HomeScore = 0;
      AwayScore = 0;
      IsFinished = false;
      Winner = SetWinner.None;
    }

    public static MatchSet Create(int setNumber, SetType type)
    {
      return new MatchSet(
        MatchSetId.New(),
        setNumber, 
        type);
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
}
