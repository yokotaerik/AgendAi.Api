using MarcAI.Application.Dtos.Costumers;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces
{
    public interface ICostumerService
    {
        Task<IEnumerable<Customer>> GetList();
        Task<Customer> GetById(Guid id);
        Task<Customer> Create(RegisterCostumerDto data);
        Task<Customer> Update(UpdateCostumerDto data);
        Task<Customer> Delete(Guid id);
    }
}
