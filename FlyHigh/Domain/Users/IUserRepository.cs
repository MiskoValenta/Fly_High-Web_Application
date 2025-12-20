using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Users;

public interface IUserRepository
{
  Task<User?> GetByIdAsync(Guid Id);
  Task<User?> GetByEmailAsync(string email);

  Task<bool> ExistsByEmailAsync(string email);

  Task AddAsync(User user);
  Task UpdateAsync(User user);
}
