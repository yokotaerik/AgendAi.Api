using MarcAI.Application.Dtos.Costumers;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CostumerDto>> GetList();
        Task<CostumerDto> GetById(Guid id);
        Task<CostumerDto> Create(RegisterCostumerDto data);
        Task<CostumerDto> Update(UpdateCostumerDto data);
        Task<CostumerDto> Delete(Guid id);
    }
}
