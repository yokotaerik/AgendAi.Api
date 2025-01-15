using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Infrastructure.Data.Repositories
{
    internal class CustomerRepository : ICostumerRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Customer>();
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
}
