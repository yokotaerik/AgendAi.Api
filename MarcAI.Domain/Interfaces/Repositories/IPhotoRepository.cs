using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IPhotoRepository
{
    void Add(Photo photo);
}
