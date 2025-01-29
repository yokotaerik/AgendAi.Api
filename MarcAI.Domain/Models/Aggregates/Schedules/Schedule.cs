using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Aggregates.Schedules;

public class Schedule : BaseEntity
{
    public DateOnly Date { get; set; }
    public Guid EmployeeId { get; set; }
    public IList<AvailablePeriod> Periods { get; private set; } = new List<AvailablePeriod>();


#pragma warning disable CS8618
    private Schedule() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618

    public static Schedule Create(DateOnly date, IList<AvailablePeriod> periods, Guid employeeId)
    {
        return new Schedule
        {
            Date = date,
            Periods = periods,
            EmployeeId = employeeId
        };
    }

    public static IList<Schedule> CreateDefault(DayOfWeek dayOfWeek, IList<AvailablePeriod> periods)
    {
        var schedules = new List<Schedule>();
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var endDate = startDate.AddMonths(6);

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek == dayOfWeek)
            {
                var schedule = new Schedule
                {
                    Date = date,
                    Periods = new List<AvailablePeriod>(periods)
                };
                schedules.Add(schedule);
            }
        }

        return schedules;
    }

    public bool MakeAttendence(DateTime startDate, DateTime endDate)
    {
        var avaiblePeriod = this.Periods.First(period => period.Start <= startDate && period.End >= endDate);

        if (avaiblePeriod == null) return false;

        if (avaiblePeriod != null) SplitPeriod(startDate, endDate, avaiblePeriod);

        return true;
    }

    private void SplitPeriod(DateTime startDate, DateTime endDate, AvailablePeriod avaiblePeriod)
    {
        this.Periods.Remove(avaiblePeriod);

        if (avaiblePeriod.Start < startDate)
            Periods.Add(AvailablePeriod.Create(avaiblePeriod.Start, startDate));

        if (avaiblePeriod.End > endDate)
            Periods.Add(AvailablePeriod.Create(endDate, avaiblePeriod.End));
    }


}
