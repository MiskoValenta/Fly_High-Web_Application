using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Teams;

public sealed class TeamRepository : ITeamRepository
{
  private readonly VolleyballDbContext _context;
  public TeamRepository(VolleyballDbContext context)
  {
    _context = context;
  }
  public async Task AddAsync(Team team)
  {
    _context.Teams.Add(team);
    await _context.SaveChangesAsync();
  }

  public async Task<bool> ExistsByIdAsync(TeamId Id)
  {
    return await _context.Teams
      .AnyAsync(t => t.Id == Id);
  }

  public async Task<Team?> GetByIdAsync(TeamId Id)
  {
    return await _context.Teams
      .FirstOrDefaultAsync(t => t.Id == Id);
  }

  public async Task<Team?> GetTeamByName(string teamName)
  {
    return await _context.Teams
      .FirstOrDefaultAsync(t => t.TeamName == teamName);
  }

  public async Task UpdateAsync(Team team)
  {
    _context.Teams.Update(team);
    await _context.SaveChangesAsync();
  }
}
