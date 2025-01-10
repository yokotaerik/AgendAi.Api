using MarcAI.Application.Dtos.Schedules.AvaiblePeriods;

namespace MarcAI.Application.Dtos.Schedules;

public record UpdateScheduleDto
{
    public Guid Id { get; init; }
    public DateOnly Date { get; init; }
    public IList<AvaiblePeriodDto> AvaiblePeriods { get; init; } = new List<AvaiblePeriodDto>();
}
