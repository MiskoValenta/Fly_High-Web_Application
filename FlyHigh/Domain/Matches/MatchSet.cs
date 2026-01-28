using Domain.Common;
using Domain.Matches.MatchEnums;
using Domain.Matches.Rules;
using System;

namespace Domain.Matches;

public class MatchSet : AuditableEntity<MatchSetId>
{
  private readonly ISetRules _rules;

  public int SetNumber { get; private set; }
  public SetType Type { get; private set; }

  public int HomeScore { get; private set; }
  public int AwayScore { get; private set; }

  public bool IsFinished { get; private set; }
  public SetWinner Winner { get; private set; }

  private MatchSet() { }

  private MatchSet(
    MatchSetId id,
    int setNumber,
    SetType type,
    ISetRules rules
  ) : base(id)
  {
    if (rules == null)
      throw new ArgumentNullException(nameof(rules));

    _rules = rules;

    SetNumber = setNumber;
    Type = type;

    HomeScore = 0;
    AwayScore = 0;

    IsFinished = false;
    Winner = SetWinner.None;
  }

  public static MatchSet Create(
    int setNumber,
    SetType type,
    ISetRules rules)
  {
    return new MatchSet(
      MatchSetId.New(),
      setNumber,
      type,
      rules
    );
  }

  public void AddPoint(SetSide side)
  {
    EnsureNotFinished();

    if (side == SetSide.Home)
      HomeScore++;
    else
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
    if (_rules.IsWinningScore(HomeScore, AwayScore, Type))
    {
      Finish(SetWinner.Home);
      return;
    }

    if (_rules.IsWinningScore(AwayScore, HomeScore, Type))
    {
      Finish(SetWinner.Away);
      return;
    }
  }

  private void Finish(SetWinner winner)
  {
    IsFinished = true;
    Winner = winner;
  }
}
