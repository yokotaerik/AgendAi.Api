using MarcAI.Application.Dtos.Auth;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MarcAI.Application.Services;

internal class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<string> Login(LoginDto data)
    {
        var user = await _userManager.FindByEmailAsync(data.Email);
        if (user == null) throw new UnauthorizedAccessException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false);
        if (user == null) throw new UnauthorizedAccessException();

        var token = _tokenService.GenerateToken(user);

        return token;
    }
}
