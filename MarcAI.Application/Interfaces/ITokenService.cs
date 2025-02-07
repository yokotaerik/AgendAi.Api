using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}