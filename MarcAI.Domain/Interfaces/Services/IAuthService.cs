using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string email, string password);
}
