using MarcAI.Application.Dtos.Photos;

namespace MarcAI.Application.Interfaces;

public interface IPhotoService
{
    Task UploadPhotoAsync(PhotoUploadDto data);
}
