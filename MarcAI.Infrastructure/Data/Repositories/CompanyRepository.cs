using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarcAI.Infrastructure.Data.Repositories;

internal class CompanyRepository : UnitOfWork, ICompanyRepository
{
    private readonly DbSet<Company> _dbSet;

    public CompanyRepository(AppDbContext context) : base(context)
    {
        _dbSet = context.Set<Company>();
    }

    public async Task Create(Company company) => await _dbSet.AddAsync(company);

    public async Task<bool> ExistsAsync(Expression<Func<Company, bool>> predicate) => await _dbSet.AnyAsync(predicate);

    public async Task<IEnumerable<Company>> GetList() => await _dbSet.AsNoTracking().Include(c => c.Photos).ToListAsync();
    public IQueryable<Company> GetCompanyQueryable()
    {
        return _dbSet.AsNoTracking();
    }
    public async Task<Guid?> GetOwnerUserId(Guid companyId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(c => c.Id == companyId)
            .SelectMany(c => c.Employees)
            .Where(e => e.Owner)
            .Select(e => e.UserId)
            .SingleOrDefaultAsync();
    }

    public async Task<Company?> GetByIdToUpdate(Guid id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

    public void Update(Company company) => _dbSet.Update(company);

    public Task<Company?> GetByUserId(Guid id) => _dbSet.Where(c => c.Employees.Any(e => e.UserId == id)).FirstOrDefaultAsync();

   
}
