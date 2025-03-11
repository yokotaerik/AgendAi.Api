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
}
