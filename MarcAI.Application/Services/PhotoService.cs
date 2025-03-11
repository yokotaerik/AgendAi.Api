using MarcAI.Application.Dtos.Photos;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Enums;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace MarcAI.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly string _photoStoragePath;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };

    public PhotoService(
        IPhotoRepository photoRepository, 
        IEmployeeRepository employeeRepository,
        IConfiguration configuration)
    {
        _photoRepository = photoRepository;
        _employeeRepository = employeeRepository;
        _photoStoragePath = configuration["PhotoStoragePath"] ?? "wwwroot/photos";
    }

    public async Task<Stream> GetPhoto(Guid photoId)
    {
        var photo = await _photoRepository.GetAsync(photoId) ?? 
            throw new FileNotFoundException("Foto não encontrada no banco de dados.");

        var filePath = photo.PathName;

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Arquivo de foto não encontrado no sistema de arquivos.");

        return new FileStream(filePath, FileMode.Open, FileAccess.Read);
    }

    public async Task UploadPhotoAsync(PhotoUploadDto data)
    {
        if (data.File == null || data.File.Length == 0)
            throw new ArgumentException("Nenhum arquivo foi enviado.");

        var extension = Path.GetExtension(data.File.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(extension))
            throw new ArgumentException($"Tipo de arquivo não permitido. Extensões permitidas: {string.Join(", ", _allowedExtensions)}");

        if (data.File.Length > 5 * 1024 * 1024) // 5MB
            throw new ArgumentException("O arquivo não pode ser maior que 5MB.");

        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(_photoStoragePath, fileName);

        Directory.CreateDirectory(_photoStoragePath);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await data.File.CopyToAsync(stream);
        }

        var photo = Photo.Create(filePath, data.EntityId, data.EntityType);
        var webUrl = $"/photos/{photo.Id}";
        photo.SetWebUrl(webUrl);

        _photoRepository.Add(photo);

        if (data.EntityType == EntitiesAssociation.Employee)
            await HandleIfUploadForEmployee(data.EntityId, photo);

        await _photoRepository.Commit();
    }

    private async Task HandleIfUploadForEmployee(Guid employeeId, Photo newPhoto)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId) ?? 
            throw new ArgumentException("Funcionário não encontrado.");

        if (employee.PhotoId.HasValue)
        {
            var oldPhoto = await _photoRepository.GetAsync(employee.PhotoId.Value);
            if (oldPhoto != null)
            {
                var oldFilePath = oldPhoto.PathName;
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                _photoRepository.Remove(oldPhoto);
            }
        }

        employee.UpdatePhoto(newPhoto.Id);
        await _employeeRepository.UpdateAsync(employee);
    }
}
