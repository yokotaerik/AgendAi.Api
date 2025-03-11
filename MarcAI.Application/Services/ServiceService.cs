using AutoMapper;
using MarcAI.Application.Dtos.Filters;
using MarcAI.Application.Dtos.Services;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;
using MarcAI.Shared.Helpers;

namespace MarcAI.Application.Services;

internal class ServiceService(IServiceRepository serviceRepository, IMapper mapper, IUserService userService, IEmployeeRepository employeeRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUserService _userService = userService;

    public async Task<ServiceDto> Create(RegisterServiceDto data)
    {
        Employee employee = await GetOwner();

        var newService = Service.Create(data.Name, data.Description, data.Duration, data.Price, employee.CompanyId);

        await _serviceRepository.Create(newService);

        if (!await _serviceRepository.Commit()) throw new Exception("Persistence Error");

        return _mapper.Map<ServiceDto>(newService);
    }


    public async Task<bool> Delete(Guid id)
    {
        await GetOwner();

        var service = await _serviceRepository.GetById(id) ?? throw new Exception("Service not found");

        service.Delete();

        _serviceRepository.Update(service);

        if (!await _serviceRepository.Commit()) throw new Exception("Persistence Error");

        return true;
    }

    public async Task<PagedResult<ServiceDto>> GetList(ServiceFilterDto filter)
    {
        var predicate = PredicateBuilder.True<Service>();

        if (!string.IsNullOrEmpty(filter.Keyword))
        {
            predicate = predicate.And(x => x.Name.Contains(filter.Keyword));
        }

        var services = await _serviceRepository.GetList(predicate, filter.Page, filter.PageSize);

        var dtos = _mapper.Map<IEnumerable<ServiceDto>>(services);

        return new PagedResult<ServiceDto>(dtos, 0, filter.Page, filter.PageSize);
    }

    public Task<ServiceDto> GetById(Guid id) => _serviceRepository.GetById(id).ContinueWith(task => _mapper.Map<ServiceDto>(task.Result));

    public async Task<ServiceDto> Update(UpdateServiceDto data)
    {
        await GetOwner();

        var serviceDb = await _serviceRepository.GetById(data.Id!.Value) ?? throw new Exception("Service not found");

        serviceDb.Update(data.Name, data.Description, data.Duration, data.Price);
        _serviceRepository.Update(serviceDb);

        if (!await _serviceRepository.Commit()) throw new Exception("Persistence Error");

        return _mapper.Map<ServiceDto>(data);
    }

    private async Task<Employee> GetOwner()
    {
        var employee = await _userService.GetCurrentEmployeIncludeUser();

        if (employee.Owner == false)
            throw new Exception("User is not an owner");
        return employee;
    }
}
