using MarcAI.Shared.Dtos.Schedules.AvaiblePeriods;

namespace MarcAI.Shared.Dtos.Schedules;

public record UpdateScheduleDto
{
    public Guid Id { get; init; }
    public DateOnly Date { get; init; }
    public IList<AvaiblePeriodDto> AvaiblePeriods { get; init; } = new List<AvaiblePeriodDto>();
}
