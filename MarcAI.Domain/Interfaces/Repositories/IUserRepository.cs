using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<string?> GetSenderName(Guid id);
    Task<User> Create(User user, string password);
    Task<User> Update(User user);
}
