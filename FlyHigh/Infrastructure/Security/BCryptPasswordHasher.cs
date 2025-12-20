using Domain.Users;
using System.Security.Cryptography;
using BCrypt.Net;

namespace Infrastructure.Security;

public sealed class BCryptPasswordHasher : IPasswordHasher
{
  private const int WorkFactor = 12;

  public string Hash(string password)
  {
    if (string.IsNullOrWhiteSpace(password))
      throw new ArgumentException("Password Cannot be empty");

    return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
  }

  public bool Verify(string password, string hash)
  {
    if (string.IsNullOrWhiteSpace(password))
      return false;

    return BCrypt.Net.BCrypt.Verify(password, hash);
  }
}
