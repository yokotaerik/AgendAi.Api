using MarcAI.Application.Dtos.Filters;
using MarcAI.Application.Dtos.Services;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Application.Interfaces;

public interface IServiceService
{
    Task<PagedResult<ServiceDto>> GetList(ServiceFilterDto filter);
    Task<ServiceDto> GetById(Guid id);
    Task<ServiceDto> Create(RegisterServiceDto service);
    Task<ServiceDto> Update(UpdateServiceDto service);
    Task<bool> Delete(Guid id);
}
