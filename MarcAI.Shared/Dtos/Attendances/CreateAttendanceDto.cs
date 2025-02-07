namespace MarcAI.Shared.Dtos.Attendances;

public record CreateAttendanceDto
{
    public Guid CostumerId { get; init; }
    public Guid EmployeeId { get; init; }
    public DateTime Appointment { get; init; }
    public IList<Guid> ServiceIds { get; init; } = new List<Guid>();
}
