using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Data.UoW;
using MarcAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Infrastructure.Data.Repositories;

internal class CustomerRepository : UnitOfWork, ICostumerRepository
{
    private readonly DbSet<Customer> _dbSet;

    public CustomerRepository(AppDbContext context) : base(context)
    {
        _dbSet = context.Set<Customer>();
    }

    public async Task Add(Customer costumer)
    {
        await _dbSet.AddAsync(costumer);
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }
}
