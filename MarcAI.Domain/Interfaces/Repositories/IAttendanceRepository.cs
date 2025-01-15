using MarcAI.Domain.Models.Aggregates.Attendances;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface IAttendanceRepository
{
    Task<Attendance> AddAsync(Attendance attendance);
}
