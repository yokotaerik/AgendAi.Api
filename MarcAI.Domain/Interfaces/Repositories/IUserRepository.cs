using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<User> Create(User user);
}
