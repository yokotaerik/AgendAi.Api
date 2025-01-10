using MarcAI.Domain.Models.Entities;
namespace MarcAI.Domain.Models.Aggregates.Attendances;

public class AttendenceService
{
    public Guid AttendenceId { get; private set; }
    public Attendance Attendance { get; private set; } = null!;
    public Guid ServiceId { get; private set; }
    public Service Service { get; private set; } = null!;
    public decimal Value { get; private set; }
}
