using Application.DTOs.Users;
using Application.Services.Users;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
  {
    try
    {
      var tokens = await _authService.RegisterAsync(request, cancellationToken);
      SetTokensInCookies(tokens.AccessToken, tokens.RefreshToken);

      return Ok(new { message = "Registration was successful" });
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message });
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
  {
    try
    {
      var tokens = await _authService.LoginAsync(request, cancellationToken);
      SetTokensInCookies(tokens.AccessToken, tokens.RefreshToken);

      return Ok(new { message = "Login was successful" });
    }
    catch (Exception ex)
    {
      return Unauthorized(new { message = ex.Message });
    }
  }

  [HttpPost("refresh")]
  public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
  {
    try
    {
      var refreshToken = Request.Cookies["refreshToken"];
      if (string.IsNullOrEmpty(refreshToken))
        return Unauthorized(new { message = "Missing Refresh Token" });

      var tokens = await _authService.RefreshTokensAsync(refreshToken, cancellationToken);
      SetTokensInCookies(tokens.AccessToken, tokens.RefreshToken);

      return Ok(new { message = "Tokeny obnoveny" });
    }
    catch (Exception ex)
    {
      return Unauthorized(new { message = ex.Message });
    }
  }

  [HttpPost("logout")]
  public IActionResult Logout()
  {
    Response.Cookies.Delete("accessToken");
    Response.Cookies.Delete("refreshToken");
    return Ok(new { message = "Logout was successful" });
  }

  [Authorize]
  [HttpGet("me")]
  public IActionResult GetMe()
  {
    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
          ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;

    var email = User.FindFirst(ClaimTypes.Email)?.Value;
    var firstName = User.FindFirst(ClaimTypes.GivenName)?.Value;
    var lastName = User.FindFirst(ClaimTypes.Surname)?.Value;

    return Ok(new
    {
      id = id,
      email = email,
      firstName = firstName,
      lastName = lastName
    });
  }

  private void SetTokensInCookies(string accessToken, string refreshToken)
  {
    var cookieOptions = new CookieOptions
    {
      HttpOnly = true,
      Secure = false, // Káre, při produkci změnit z false na true
      SameSite = SameSiteMode.Lax,
      Expires = DateTime.UtcNow.AddMinutes(1)
    };

    var refreshCookieOptions = new CookieOptions
    {
      HttpOnly = true,
      Secure = false,
      SameSite = SameSiteMode.Lax,
      Expires = DateTime.UtcNow.AddDays(7)
    };

    Response.Cookies.Append("accessToken", accessToken, cookieOptions);
    Response.Cookies.Append("refreshToken", refreshToken, refreshCookieOptions);
  }
}