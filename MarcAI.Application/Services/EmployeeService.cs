using MarcAI.Application.Dtos.Employees;
using MarcAI.Application.Dtos.Filters;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Services;

internal class EmployeeService : IEmployeeService
{
    private ICompanyRepository _companyRepository;
    private IUserRepository _userRepository;
    private IEmployeeRepository _employeeRepository;

    public EmployeeService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IUserRepository userRepository)
    {
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
    }
    public Task<Employee> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetList(EmployeeFilterDto filter)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> Create(RegisterEmployeeDto data)
    {



        var company = await _companyRepository.GetById(data.CompanyId) ?? 
            throw new ArgumentException("Company not found.");

        var newUserToEmployee = User.Create(data.Email);

        await _userRepository.Create(newUserToEmployee, data.Password);

        var isFirstEmployee = company.Employees.Count == 0;

        Employee? newEmployee;

        if (isFirstEmployee)
            newEmployee = Employee.Create(data.Name, data.Surname, data.Cpf, newUserToEmployee.Id, company, true);
        else
            newEmployee = Employee.Create(data.Name, data.Surname, data.Cpf, newUserToEmployee.Id, company, false);

        return await _employeeRepository.AddAsync(newEmployee);
    }
    public async Task<Employee> Update(UpdateEmployeeDto data)
    {
        var employee = await _employeeRepository.GetByIdAsync(data.Id) ??
            throw new ArgumentException("Employee not found.");

        employee.Update(data.Name, data.Surname);

        return await _employeeRepository.UpdateAsync(employee);
    }

    public Task<Employee> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
