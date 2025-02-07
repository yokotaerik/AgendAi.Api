using MarcAI.Shared.Dtos.Schedules.AvaiblePeriods;

namespace MarcAI.Shared.Dtos.Schedules;
public record RegisterDefaultSchedulesDto
{
    public Guid EmployeeId { get; init; }
    public Dictionary<DayOfWeek, List<AvaiblePeriodDto>> DefaultPeriods { get; init; } = new Dictionary<DayOfWeek, List<AvaiblePeriodDto>>();
}
