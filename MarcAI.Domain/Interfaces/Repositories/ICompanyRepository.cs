using MarcAI.Domain.Models.Entities;
using System.Linq.Expressions;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<bool> ExistsAsync(Expression<Func<Company, bool>> predicate);
    Task<Company?> GetById(Guid id);
    Task<Company?> GetByIdToUpdate(Guid id);
    Task<Company> Create(Company company);
    Task<Company> Update(Company company);
    Task<bool> Delete(Guid id);
}
