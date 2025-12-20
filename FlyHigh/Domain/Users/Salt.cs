using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Users;

public record Salt
{
  private const int SaltLength = 16;
  public string Value { get; init; }
  private Salt(string value) => Value = value;
  public static Salt Create(string value)
  {
    return new Salt(value);
  }
  public static Salt Generate()
  {
    byte[] saltBytes = new byte[SaltLength];
    
    using (var rng = RandomNumberGenerator.Create())
    {
      rng.GetNonZeroBytes(saltBytes);
    }

    return new Salt(Convert.ToBase64String(saltBytes));
  }

  // accidentally printing the record type name
  public override string ToString() => Value;
}
