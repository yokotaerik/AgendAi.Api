using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Infrastructure.Data.Repositories;

internal class MessageRepository : UnitOfWork, IMessageRepository
{
    private readonly AppDbContext _dbContext;

    public MessageRepository(AppDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Message>().FindAsync(id);
    }

    public async Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(string companyId, string customerId)
    {
        //Create Guids
        var companyGuid = new Guid(companyId);
        var customerGuid = new Guid(customerId);


        return await _dbContext.Set<Message>()
            .Where(m => (m.SenderId == companyGuid && m.ReceiverId == customerGuid) ||
                       (m.SenderId == customerGuid && m.ReceiverId == companyGuid))
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Message message)
    {
        await _dbContext.Set<Message>().AddAsync(message);
    }
}
