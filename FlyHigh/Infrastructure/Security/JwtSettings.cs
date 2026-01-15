using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Security;

public class JwtSettings
{
  public string Secret { get; set; } = string.Empty;
  public int ExpiryMinutes { get; set; } = 60;
}
