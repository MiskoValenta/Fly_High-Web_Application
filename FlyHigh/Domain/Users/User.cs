using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Users;

public class User : AuditableEntity<UserId>
{
  public string FirstName { get; private set; } = string.Empty;
  public string LastName { get; private set; } = string.Empty;
  public string Email { get; private set; } = string.Empty;
  public string HashedPassword { get; private set; } = string.Empty;
  public DateTime? LastLogged { get; private set; }
  public DateTime CreatedAt { get; private set; }

  private User() { }

  private User(
      UserId id,
      string firstName,
      string lastName,
      string email,
      string hashedPassword,
      DateTime createdAt) : base(id)
  {
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
        UserId.New(),
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
