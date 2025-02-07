namespace MarcAI.Shared.Dtos.Schedules.AvaiblePeriods;

public record AvaiblePeriodDto
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
}
