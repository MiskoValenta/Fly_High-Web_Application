using Domain.Entities.Matches;
using Domain.Value_Objects.Matches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories.Matches;

public interface IMatchRepository
{
  Task<Match?> GetByIdAsync(MatchId id, CancellationToken cancellationToken = default);
  Task AddAsync(Match match, CancellationToken cancellationToken = default);
  void Update(Match match);
}
