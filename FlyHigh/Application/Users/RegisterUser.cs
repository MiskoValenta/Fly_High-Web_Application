using System;
using System.Collections.Generic;
using System.Text;
using Domain.Users;

namespace Application.Users;

public sealed class UserRegistrationService
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;
  
  public UserRegistrationService(IUserRepository userRepository, IPasswordHasher passwordHasher)
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
  }

  public async Task RegisterAsync(
    string firstName,
    string lastName,
    string email,
    string password)
  {
    ValidateInput(firstName, lastName, email, password);

    email = email.Trim().ToLowerInvariant();

    if (await _userRepository.ExistsByEmailAsync(email))
      throw new InvalidOperationException("User with this email alread exists");

    string passwordHash = _passwordHasher.Hash(password);

    var user = User.Create(
      firstName,
      lastName,
      email,
      passwordHash);

    await _userRepository.AddAsync(user);
  }
  private static void ValidateInput(
    string firstName,
    string lastName,
    string email,
    string password)
  {
    if (string.IsNullOrWhiteSpace(firstName))
      throw new InvalidOperationException("First name cannot be empty.");

    if (string.IsNullOrWhiteSpace(lastName))
      throw new InvalidOperationException("Last name cannot be empty.");

    if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
      throw new InvalidOperationException("Invalid email address.");

    if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
      throw new InvalidOperationException("Password must be at least 6 characters long.");

    if (!password.Any(char.IsUpper))
      throw new InvalidOperationException("Password must contain at least one uppercase letter.");

    if (!password.Any(char.IsDigit))
      throw new InvalidOperationException("Password must contain at least one digit.");
  }
}
