using MarcAI.Application.Dtos.Schedules.AvaiblePeriods;

namespace MarcAI.Shared.Dtos.Schedules;

public record RegisterSchedulesDto
{
    public List<RegisterScheduleDto> Schedules { get; init; } = new List<RegisterScheduleDto>();
    public Guid EmployeeId { get; init; }
}
