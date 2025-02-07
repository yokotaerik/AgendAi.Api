using MarcAI.Shared.Dtos.Schedules.AvaiblePeriods;

namespace MarcAI.Shared.Dtos.Schedules
{
    public record RegisterScheduleDto
    {
        public IList<AvaiblePeriodDto> AvaiblePeriods { get; init; } = new List<AvaiblePeriodDto>();
        public DateOnly Date { get; init; }
    }
}