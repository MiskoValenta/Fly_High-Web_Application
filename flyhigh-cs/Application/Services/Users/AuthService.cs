using Application.Common.Interfaces;
using Application.DTOs.Users;
using Application.Interfaces.Users;
using Domain.Entities.Users;
using Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Users.Exceptions;

namespace Application.Services.Users;

public class AuthService : IAuthService
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IJwtProvider _jwtProvider;
  private readonly IUnitOfWork _unitOfWork;

  public AuthService(
      IUserRepository userRepository,
      IPasswordHasher passwordHasher,
      IJwtProvider jwtProvider,
      IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
    _jwtProvider = jwtProvider;
    _unitOfWork = unitOfWork;
  }

  private AuthTokens GenerateTokensAndUpdateUser(User user)
  {
    var accessToken = _jwtProvider.GenerateAccessToken(user);
    var refreshToken = _jwtProvider.GenerateRefreshToken();

    user.UpdateRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));
    return new AuthTokens(accessToken, refreshToken);
  }

  public async Task<AuthTokens> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
  {
    if (await _userRepository.GetByEmailAsync(request.Email, cancellationToken) != null)
      throw new AuthEmailException();

    var hash = _passwordHasher.Hash(request.Password);
    var user = User.Create(
        firstName: request.FirstName,
        lastName: request.LastName,
        email: request.Email,
        passwordHash: hash);

    var tokens = GenerateTokensAndUpdateUser(user);

    await _userRepository.AddAsync(user, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return tokens;
  }

  public async Task<AuthTokens> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
  {
    var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

    if (user == null)
      throw new AuthBadCredentials();

    if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
      throw new AuthBadCredentials();

    var tokens = GenerateTokensAndUpdateUser(user);

    await _userRepository.UpdateAsync(user, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return tokens;
  }

  public async Task<AuthTokens> RefreshTokensAsync(string refreshToken, CancellationToken cancellationToken = default)
  {
    var user = await _userRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);

    if (user == null)
      throw new AuthInvalidRefreshToken();

    if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
      throw new AuthExpiredRefreshToken();

    var tokens = GenerateTokensAndUpdateUser(user);

    await _userRepository.UpdateAsync(user, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return tokens;
  }
}
