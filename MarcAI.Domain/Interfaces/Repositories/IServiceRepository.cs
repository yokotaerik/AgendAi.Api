using MarcAI.Domain.Models.Entities;
using System.Linq.Expressions;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IServiceRepository : IUnitOfWork
{
    Task<Service?> GetById(Guid id);
    Task<IEnumerable<Service>> GetByIds(List<Guid> ids);
    Task<IEnumerable<Service>> GetList(Expression<Func<Service, bool>> predicate, int pageNumber, int pageSize);
    Task<IEnumerable<Service>> GetListByIds(IList<Guid> ids);
    Task Create(Service service);
    void Update(Service service);
}
