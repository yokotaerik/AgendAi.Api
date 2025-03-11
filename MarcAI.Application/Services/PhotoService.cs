using MarcAI.Application.Dtos.Photos;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using Microsoft.Extensions.Configuration;


namespace MarcAI.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly string _photoStoragePath;

    public PhotoService(IPhotoRepository photoRepository, IConfiguration configuration)
    {
        _photoRepository = photoRepository;
        _photoStoragePath = configuration["PhotoStoragePath"] ?? "wwwroot/photos";
    }

    public async Task UploadPhotoAsync(PhotoUploadDto data)
    {
        if (data.File == null || data.File.Length == 0)
            throw new ArgumentException("No file uploaded.");

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(data.File.FileName)}";
        var filePath = Path.Combine(_photoStoragePath, fileName);

        if (!Directory.Exists(_photoStoragePath))
        {
            Directory.CreateDirectory(_photoStoragePath);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await data.File.CopyToAsync(stream);
        }

        var photoUrl = $"/photos/{fileName}";

        var photo = Photo.Create(photoUrl, data.EntityId, data.EntityType);

        _photoRepository.Add(photo);
    }
}
