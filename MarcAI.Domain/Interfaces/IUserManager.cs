
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces;

public interface IUserManager
{
    Task<bool> CreateUserAsync(User user, string password);
}
