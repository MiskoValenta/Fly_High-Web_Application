using Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users;

public interface ITokenService
{
  string GenerateToken(User user);
}
