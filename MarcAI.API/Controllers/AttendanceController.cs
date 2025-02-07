using MarcAI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MarcAI.Application.Dtos.Filters;

namespace MarcAI.API.Controllers
{
    [Route("api/[controller]")]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(AttendanceFilterDto filter)
        {
            try
            {
                var attendances = await _attendanceService.GetList(filter);
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
