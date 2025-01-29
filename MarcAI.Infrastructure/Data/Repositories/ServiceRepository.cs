using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarcAI.Infrastructure.Data.Repositories;

internal class ServiceRepository(AppDbContext context) : UnitOfWork(context), IServiceRepository
{
    private readonly DbSet<Service> _dbSet = context.Set<Service>();

    public async Task Create(Service Service) => await _dbSet.AddAsync(Service);

    public async Task<bool> ExistsAsync(Expression<Func<Service, bool>> predicate) => await _dbSet.AnyAsync(predicate);

    public async Task<Service?> GetById(Guid id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Service?> GetByIdToUpdate(Guid id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Service>> GetListByIds(IList<Guid> ids)
         => await _dbSet.Where(s => ids.Contains(s.Id)).ToListAsync();

    public void Update(Service Service) =>  _dbSet.Update(Service);
}
