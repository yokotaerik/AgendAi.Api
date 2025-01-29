using MarcAI.Domain.Models.Entities;
using System.Linq.Expressions;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface ICompanyRepository : IUnitOfWork
{
    Task<bool> ExistsAsync(Expression<Func<Company, bool>> predicate);
    Task<Company?> GetById(Guid id);
    Task<Company?> GetByIdToUpdate(Guid id);
    Task Create(Company company);
    void Update(Company company);
}
