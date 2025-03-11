using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IPhotoRepository : IUnitOfWork
{
    Task<Photo?> GetAsync(Guid id);   
    void Add(Photo photo);
    void Remove(Photo photo);
}
