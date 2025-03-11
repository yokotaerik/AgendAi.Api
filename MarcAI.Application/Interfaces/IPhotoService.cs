using MarcAI.Application.Dtos.Photos;
using MarcAI.Domain.Interfaces;

namespace MarcAI.Application.Interfaces;

public interface IPhotoService
{
    Task UploadPhotoAsync(PhotoUploadDto data);
    Task<Stream> GetPhoto(Guid photoId);
}
