using MarcAI.Application.Dtos.Attendances;
using MarcAI.Application.Dtos.Filters;
using MarcAI.Domain.Models.Aggregates.Attendances;

namespace MarcAI.Application.Interfaces;

public interface IAttendanceService
{
    Task<IEnumerable<AttendanceDto>> GetList(AttendanceFilterDto filter);
    Task<Attendance> CreateAttenndance(CreateAttendanceDto data);
}
