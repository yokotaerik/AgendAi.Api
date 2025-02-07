using AutoMapper;
using MarcAI.Application.Dtos.Filters;
using MarcAI.Application.Dtos.Services;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;
using MarcAI.Shared.Helpers;

namespace MarcAI.Application.Services;

internal class ServiceService(IServiceRepository serviceRepository, IMapper mapper) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceDto> Create(RegisterServiceDto service)
    {
        var newService = Service.Create(service.Name, service.Description, service.Duration ,service.Price, service.CompanyId);

        await _serviceRepository.Create(newService);

        if(!await _serviceRepository.Commit()) throw new Exception("Persistence Error");

        return _mapper.Map<ServiceDto>(service);
    }

    public async Task<bool> Delete(Guid id)
    {
        var service =  await _serviceRepository.GetById(id) ?? throw new Exception("Service not found");

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

    public async Task<ServiceDto> Update(UpdateServiceDto service)
    {
        var serviceDb = await _serviceRepository.GetById(service.Id!.Value) ?? throw new Exception("Service not found");

        serviceDb.Update(service.Name, service.Description, service.Duration, service.Price);
        _serviceRepository.Update(serviceDb);
        
        if (!await _serviceRepository.Commit()) throw new Exception("Persistence Error");

        return _mapper.Map<ServiceDto>(service);
    }

  
}
