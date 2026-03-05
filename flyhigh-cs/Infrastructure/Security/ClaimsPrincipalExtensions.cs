using Domain.Value_Objects.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Security;

public static class ClaimsPrincipalExtensions
{
  public static UserId GetUserId(this ClaimsPrincipal user)
  {
    if (user == null)
      throw new ArgumentNullException(nameof(user));

    var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                      ?? user.FindFirst("sub")?.Value;

    if (userIdClaim == null)
      throw new Exception("User ID claim missing in token.");

    return new UserId(Guid.Parse(userIdClaim));
  }
}
