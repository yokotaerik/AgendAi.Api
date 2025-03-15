using MarcAI.Domain.Models.Entities;
using System.Linq.Expressions;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface ICompanyRepository : IUnitOfWork
{
    Task<bool> ExistsAsync(Expression<Func<Company, bool>> predicate);
    Task<IEnumerable<Company>> GetList();
    Task<Guid?> GetOwnerUserId(Guid companyId);
    IQueryable<Company> GetCompanyQueryable();
    Task<Company?> GetByUserId(Guid id);
    Task<Company?> GetByIdToUpdate(Guid id);
    Task Create(Company company);
    void Update(Company company);
}
