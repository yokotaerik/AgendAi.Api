using MarcAI.Application.Dtos.Photos;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MarcAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotosController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadPhoto([FromForm] PhotoUploadDto data)
    {
        try
        {
            await _photoService.UploadPhotoAsync(data);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{photoId}")]
    public async Task<IActionResult> GetPhoto(Guid photoId)
    {
        try
        {
            var photo = await _photoService.GetPhoto(photoId);
            if (photo == null)
                return NotFound();

            return Ok(photo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
