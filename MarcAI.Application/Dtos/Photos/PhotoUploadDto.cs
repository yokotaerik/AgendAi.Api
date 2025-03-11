using MarcAI.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace MarcAI.Application.Dtos.Photos

public class PhotoUploadDto
{
    public IFormFile File { get; set; }
    public Guid EntityId { get; set; }
    public EntitiesAssociation EntityType { get; set; }
}
