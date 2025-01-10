using MarcAI.Application.Dtos.Employees;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetList();
    Task<Employee> GetById(Guid id);
    Task<Employee> Create(RegisterEmployeeDto data);
    Task<Employee> Update(UpdateEmployeeDto data);
    Task<Employee> Delete(Guid id);
}
