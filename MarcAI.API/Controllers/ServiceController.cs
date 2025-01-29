using MarcAI.Application.Dtos.Services;
using MarcAI.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarcAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var services = await _serviceService.GetAll();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var service = await _serviceService.GetById(id);
                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterServiceDto service)
        {
            try
            {
                var createdService = await _serviceService.Create(service);
                return CreatedAtAction(nameof(GetById), new { id = createdService.Id }, createdService);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateServiceDto service)
        {
            try
            {
                var updatedService = await _serviceService.Update(service);
                return Ok(updatedService);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _serviceService.Delete(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
