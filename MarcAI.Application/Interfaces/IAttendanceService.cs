using MarcAI.Application.Dtos.Attendances;
using MarcAI.Domain.Models.Aggregates.Attendances;

namespace MarcAI.Application.Interfaces;

public interface IAttendanceService
{
    Task<Attendance> CreateAttenndance(CreateAttendanceDto data);
}
