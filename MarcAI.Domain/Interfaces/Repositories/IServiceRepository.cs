using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IServiceRepository : IUnitOfWork
{
    Task<Service?> GetById(Guid id);
    Task<IEnumerable<Service>> GetListByIds(IList<Guid> ids);
    Task Create(Service service);
    void Update(Service service);
}
