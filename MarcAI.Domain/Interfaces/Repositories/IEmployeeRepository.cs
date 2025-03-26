using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IEmployeeRepository : IUnitOfWork
{
    Task<Employee?> GetByIdAsync(Guid id);
    Task<Employee?> GetByCpfAsync(string cpf);
    Task<Employee?> GetByEmailAsync(string email);
    Task<Employee?> GetByUserIdAsync(Guid userId);
    Task<Employee?> GetByCompanyIdAsync(Guid companyId);
    IQueryable<Employee> GetQueryable();
    Task<Employee> AddAsync(Employee employee);
    Task<Employee> UpdateAsync(Employee employee);
}
