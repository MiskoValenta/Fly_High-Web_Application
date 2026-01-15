using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Matches.MatchEnums;

public enum MatchStatus
{
  Scheduled,
  InProgress,
  Finished,
  Cancelled
}
