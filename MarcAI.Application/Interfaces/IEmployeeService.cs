using MarcAI.Application.Dtos.Common.User;
using MarcAI.Application.Dtos.Employees;
using MarcAI.Application.Dtos.Filters;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<BasicInfoDto>> GetList(EmployeeFilterDto filter);
    Task<EmployeeDto> GetById(Guid id);
    Task<EmployeeDto> GetByUserId(Guid id);
    Task<EmployeeDto> GetByEmail(string email);
    Task Create(RegisterEmployeeDto data);
    Task<Employee> Create(RegisterEmployeeDto data, Guid companyId);
    Task<Employee> Update(UpdateEmployeeDto data);
    Task Delete(Guid id);
}
