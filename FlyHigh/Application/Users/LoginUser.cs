using Domain.Users;
using System;
using System.Threading.Tasks;

namespace Application.Users;

public sealed class LoginUser
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly ITokenService _tokenService;

  public LoginUser(
      IUserRepository userRepository,
      IPasswordHasher passwordHasher,
      ITokenService tokenService)
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
    _tokenService = tokenService;
  }

  public async Task<string> LoginAsync(string email, string password)
  {
    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
      throw new ArgumentException("Email and password must be provided.");

    email = email.Trim().ToLowerInvariant();

    var user = await _userRepository.GetByEmailAsync(email);
    if (user == null)
      throw new UnauthorizedAccessException("Invalid credentials.");

    bool isValid = _passwordHasher.Verify(password, user.HashedPassword);
    if (!isValid)
      throw new UnauthorizedAccessException("Invalid credentials.");

    string token = _tokenService.GenerateToken(user);
    return token;
  }
}
