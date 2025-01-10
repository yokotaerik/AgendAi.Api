namespace MarcAI.Domain.Models.Aggregates.Schedules;

public class AvailablePeriod
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
}
