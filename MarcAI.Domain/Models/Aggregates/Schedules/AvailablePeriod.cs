using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Aggregates.Schedules;

public class AvailablePeriod : BaseEntity
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public Guid ScheduleId { get; private set; }

    public static AvailablePeriod Create(DateTime start, DateTime end)
    {
        return new AvailablePeriod
        {
            Start = start,
            End = end
        };
    }

#pragma warning disable CS8618
    private AvailablePeriod() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618
}
