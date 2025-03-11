using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MarcAI.Application.Services;

internal class UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IEmployeeRepository employeeRepository) : IUserService
{
    public async Task<User> GetCurrentUserAsync()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst("Id")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        // Usa o repositório para obter o usuário
        var user = await userRepository.GetById(new Guid(userId));

        return user ?? throw new UnauthorizedAccessException("Usuário não encontrado.");
    }

    public async Task<Employee> GetCurrentEmployeIncludeUser()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var employee = await employeeRepository.GetByUserIdAsync(new Guid(userId));

        return employee ?? throw new UnauthorizedAccessException("Usuário não é um funcionário.");
    }

    public Guid GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst("Id")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        return new Guid(userId);
    }
}
