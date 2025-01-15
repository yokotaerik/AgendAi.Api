using MarcAI.Application.Dtos.Services;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Services;

internal class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<Service> Create(RegisterServiceDto service)
    {
        var newService = Service.Create(service.Name, service.Description, service.Duration ,service.Price, service.CompanyId);

        return await _serviceRepository.Create(newService);
    }

    public async Task<bool> Delete(Guid id)
    {
        var service =  await _serviceRepository.GetById(id) ?? throw new Exception("Service not found");

        service.Delete();

        await _serviceRepository.Update(service);

        return true;
    }

    public Task<IEnumerable<ServiceDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Service> Update(UpdateServiceDto service)
    {
        var serviceDb = await _serviceRepository.GetById(service.Id!.Value) ?? throw new Exception("Service not found");

        serviceDb.Update(service.Name, service.Description, service.Duration, service.Price);

        return await _serviceRepository.Update(serviceDb);
    }
}
