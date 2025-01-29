using AutoMapper;
using MarcAI.Application.Dtos.Services;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;

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

    public Task<IEnumerable<ServiceDto>> GetAll()
    {
        throw new NotImplementedException();
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
