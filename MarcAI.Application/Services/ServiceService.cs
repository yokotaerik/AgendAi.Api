using MarcAI.Application.Dtos.Services;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Services;

internal class ServiceService : IServiceService
{
    public Task<Service> Create(RegisterServiceDto service)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ServiceDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Service> Update(UpdateServiceDto service)
    {
        throw new NotImplementedException();
    }
}
