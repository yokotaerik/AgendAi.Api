using AutoMapper;
using MarcAI.Application.Dtos.Employees;
using MarcAI.Application.Dtos.Filters;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Enums.User;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Services;

internal class EmployeeService : IEmployeeService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IUserRepository userRepository, IUserService userService, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _userService = userService;
        _mapper = mapper;
    }

    public Task<EmployeeDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeDto> GetByUserId(Guid id)
    {
        return _mapper.Map<EmployeeDto>(await _employeeRepository.GetByUserIdAsync(id));
    }

    public async Task<EmployeeDto> GetByEmail(string email)
    {
        return _mapper.Map<EmployeeDto>(await _employeeRepository.GetByEmailAsync(email));
    }

    public Task<IEnumerable<Employee>> GetList(EmployeeFilterDto filter)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> Create(RegisterEmployeeDto data)
    {
        var userId = _userService.GetUserId();

        var company = await _companyRepository.GetByUserId(userId) ?? 
            throw new ArgumentException("Company not found.");

        var newUserToEmployee = User.Create(data.Name, data.Surname,data.Email, UserType.Employee);

        await _userRepository.Create(newUserToEmployee, data.Password);

        var newEmployee = Employee.Create(data.Cpf, newUserToEmployee.Id, company.Id, false);

        return await _employeeRepository.AddAsync(newEmployee);
    }

    public async Task<Employee> Create(RegisterEmployeeDto data, Guid companyId)
    {
        var newUserToEmployee = User.Create(data.Name, data.Surname, data.Email, UserType.Employee);

        await _userRepository.Create(newUserToEmployee, data.Password);

        var newEmployee = Employee.Create(data.Cpf, newUserToEmployee.Id, companyId, true);

        return await _employeeRepository.AddAsync(newEmployee);
    }

    public async Task<Employee> Update(UpdateEmployeeDto data)
    {
        var employee = await _employeeRepository.GetByIdAsync(data.Id) ??
            throw new ArgumentException("Employee not found.");

        return await _employeeRepository.UpdateAsync(employee);
    }

    public Task<Employee> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
