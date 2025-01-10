using MarcAI.Application.Dtos.Services;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetAll();
    Task<ServiceDto> GetById(Guid id);
    Task<Service> Create(RegisterServiceDto service);
    Task<Service> Update(UpdateServiceDto service);
    Task<bool> Delete(Guid id);
}
