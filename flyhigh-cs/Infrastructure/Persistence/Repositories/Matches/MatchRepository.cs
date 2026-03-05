using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Matches;
using Domain.Repositories.Matches;
using Domain.Value_Objects.Matches;

namespace Infrastructure.Persistence.Repositories.Matches;

public class MatchRepository : IMatchRepository
{
  private readonly FlyHighDbContext _context;

  public MatchRepository(FlyHighDbContext context)
  {
    _context = context;
  }

  public Task AddAsync(Match match, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<Match?> GetByIdAsync(MatchId id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public void Update(Match match)
  {
    throw new NotImplementedException();
  }
}
