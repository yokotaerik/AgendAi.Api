using AutoMapper;
using MarcAI.Application.Dtos.Common.User;
using MarcAI.Application.Dtos.Employees;
using MarcAI.Application.Dtos.Filters;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Enums.User;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Application.Services;

internal class EmployeeService : IEmployeeService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IServiceRepository _serviceRepositoy;
    private readonly IMapper _mapper;

    public EmployeeService(ICompanyRepository companyRepository,
                           IEmployeeRepository employeeRepository,
                           IUserRepository userRepository,
                           IUserService userService,
                           IMapper mapper,
                           IServiceRepository serviceRepositoy)
    {
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _userService = userService;
        _mapper = mapper;
        _serviceRepositoy = serviceRepositoy;
    }

    public async Task<EmployeeDto> GetById(Guid id)
    {
        return _mapper.Map<EmployeeDto>(await _employeeRepository.GetByIdAsync(id));
    }

    public async Task<EmployeeDto> GetByUserId(Guid id)
    {
        return _mapper.Map<EmployeeDto>(await _employeeRepository.GetByUserIdAsync(id));
    }

    public async Task<EmployeeDto> GetByEmail(string email)
    {
        return _mapper.Map<EmployeeDto>(await _employeeRepository.GetByEmailAsync(email));
    }

    public async Task<IEnumerable<BasicInfoDto>> GetList(EmployeeFilterDto filter)
    {
        var query = _employeeRepository.GetQueryable();

        if (filter.CompanyId != null)
            query = query.Where(e => e.CompanyId == filter.CompanyId);

        return await query.Select(
            e => new BasicInfoDto(e.Id,e.Photo != null ? e.Photo!.WebUrl : "", e.User.Name + " " + e.User.Surname))
            .ToListAsync();
    }

    public async Task Create(RegisterEmployeeDto data)
    {
        var userId = _userService.GetUserId();

        var company = await _companyRepository.GetByUserId(userId) ?? 
            throw new ArgumentException("Company not found.");

        var newUserToEmployee = User.Create(data.Name, data.Surname,data.Email, UserType.Employee);

        await _userRepository.Create(newUserToEmployee, data.Password);

        var newEmployee = Employee.Create("", newUserToEmployee.Id, company.Id, false);
    
        await _employeeRepository.AddAsync(newEmployee);

        var success = await _employeeRepository.Commit();
        if (!success)
            throw new Exception("Commit failed.");
    }

    public async Task<Employee> Create(RegisterEmployeeDto data, Guid companyId)
    {
        var newUserToEmployee = User.Create(data.Name, data.Surname, data.Email, UserType.Employee);

        await _userRepository.Create(newUserToEmployee, data.Password);

        var newEmployee = Employee.Create("", newUserToEmployee.Id, companyId, true);

        return await _employeeRepository.AddAsync(newEmployee);
    }

    public async Task<Employee> Update(UpdateEmployeeDto data)
    {
        var employee = await _employeeRepository.GetByIdAsync(data.Id) ??
            throw new ArgumentException("Employee not found.");

        var user = employee.User;

        if (String.IsNullOrEmpty(data.Email))
        {
            throw new ArgumentException("Email is required.");
        }

        user.Update(data.Name ?? "", data.Surname ?? "", data.Email);

        await _userRepository.Update(user);

        var services = await _serviceRepositoy.GetListByIds(data.ServicesIds);

        employee.AddOfferedServices(services.ToList());

        await _employeeRepository.UpdateAsync(employee);

        var success = await _employeeRepository.Commit();
        if (!success)
            throw new Exception("Commit failed.");

        return employee;
    }

    public async Task Delete(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id) ??
            throw new ArgumentException("Employee not found.");

        employee.Delete();

        await _employeeRepository.UpdateAsync(employee);

        var success = await _employeeRepository.Commit();
        if (!success)
            throw new Exception("Commit failed.");
    }
}
