using MarcAI.Application.Dtos.Auth;

namespace MarcAI.Application.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginDto data);
}
