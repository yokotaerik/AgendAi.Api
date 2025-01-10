using MarcAI.Application.Dtos.Schedules.AvaiblePeriods;

namespace MarcAI.Application.Dtos.Schedules
{
    public record RegisterScheduleDto
    {
        public IList<AvaiblePeriodDto> AvaiblePeriods { get; init; } = new List<AvaiblePeriodDto>();
        public DateOnly Date { get; init; }
    }
}