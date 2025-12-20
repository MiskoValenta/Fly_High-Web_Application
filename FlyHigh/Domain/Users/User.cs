using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Users;

public class User
{
  public Guid Id { get; private set; }
  public string FirstName { get; private set; } = string.Empty;
  public string LastName { get; private set; } = string.Empty;
  public string Email { get; private set; } = string.Empty;
  public string HashedPassword { get; private set; } = string.Empty;
  public DateTime CreatedAt { get; private set; }

  private User() { }

  private User(
      Guid id,
      string firstName,
      string lastName,
      string email,
      string hashedPassword,
      DateTime createdAt)
  {
    Id = id;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    HashedPassword = hashedPassword;
    CreatedAt = createdAt;
  }

  public static User Create(
      string firstName,
      string lastName,
      string email,
      string hashedPassword)
  {
    return new User(
        Guid.NewGuid(),
        firstName,
        lastName,
        email,
        hashedPassword,
        DateTime.UtcNow
    );
  }

  public void ChangeHashedPassword(string newHashedPassword)
  {
    HashedPassword = newHashedPassword;
  }
}
