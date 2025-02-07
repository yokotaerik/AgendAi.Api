using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MarcAI.Application.Services;

internal class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        // Usa o repositório para obter o usuário
        return await _userRepository.GetById(new Guid(userId));
    }
}
