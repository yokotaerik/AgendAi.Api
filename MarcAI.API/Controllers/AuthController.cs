using MarcAI.Application.Dtos.Auth;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarcAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto data)
    {
        try
        {
            var token = await _authService.AuthenticateAsync(data.Email, data.Password);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        try
        {
            var user = await _userService.GetCurrentUserAsync();
            return Ok(new { user.Name, user.Surname, user.Email, user.Role });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("claims")]
    public IActionResult GetClaims()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Ok(claims);
    }
}
