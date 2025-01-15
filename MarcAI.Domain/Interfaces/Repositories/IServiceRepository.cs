using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<Service> Create(Service service);
    Task<bool> Delete(Guid id);
    Task<Service> GetById(Guid id);
    Task<IEnumerable<Service>> GetListByIds(IList<Guid> ids);
    Task<Service> Update(Service service);
}
