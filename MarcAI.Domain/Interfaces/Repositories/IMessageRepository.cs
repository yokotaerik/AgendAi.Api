using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IMessageRepository : IUnitOfWork
{
    IQueryable<Message> GetQueryable();
    Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(string user1Id, string user2Id);
    Task AddAsync(Message message);
}
