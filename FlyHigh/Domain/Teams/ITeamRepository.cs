using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Teams;

public interface ITeamRepository
{
  Task<Team?> GetByIdAsync(TeamId Id);
  Task<Team?> GetTeamByName(string teamName);
  Task<bool> ExistsByIdAsync(TeamId Id);
  Task AddAsync(Team team);
  Task UpdateAsync(Team team);
}
