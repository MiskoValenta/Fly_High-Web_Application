using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Users;

public class User
{
  public UserId Id { get; private set; }
  public string FirstName { get; private set; } = string.Empty;
  public string LastName { get; private set; } = string.Empty;
  public string Email { get; private set; } = string.Empty;
  public string HashedPassword { get; private set; } = string.Empty;
  public DateTime CreatedAt { get; private set; }

  private User() { }

  private User(
      UserId id,
      string firstName,
      string lastName,
      string email,
      string hashedPassword)
  {
    Id = id;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    HashedPassword = hashedPassword;
    CreatedAt = DateTime.UtcNow;
  }

  public static User Create(
      string firstName,
      string lastName,
      string email,
      string hashedPassword)
  {
    return new User(
        UserId.New(),
        firstName,
        lastName,
        email,
        hashedPassword
    );
  }

  public void ChangeHashedPassword(string newHashedPassword)
  {
    HashedPassword = newHashedPassword;
  }
}
