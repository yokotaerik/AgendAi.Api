using MarcAI.Domain.Models.Aggregates.Schedules;
using MarcAI.Domain.Models.Common;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Domain.Models.Entities;
public class Employee : BaseEntity
{
    public Cpf? Cpf { get; private set; }
    public Guid? PhotoId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CompanyId { get; private set; }
    public bool Owner { get; private set; }
    public IList<Schedule> Schedules { get; private set; } = new List<Schedule>();
    public IList<Service> OfferedServices { get; private set; } = new List<Service>();
    public Photo? Photo { get; }

    // Ef Core Relational
    public Company Company {  get; private set; } = null!;
    public User User { get; private set; } = null!;

#pragma warning disable CS8618
    private Employee() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618

    private Employee(Cpf? cpf, Guid userId, Guid companyId, bool owner) : base()
    {
        Cpf = cpf;
        UserId = userId;
        CompanyId = companyId;
        Owner = owner;
    }

    public static Employee Create(string? cpf, Guid userId, Guid companyId, bool owner)
    {
        Cpf? cpfVo = null;

        if (cpf != null)
            cpfVo = Cpf.Create(cpf);

        return new Employee(cpfVo, userId, companyId, owner);
    }

    public void AddDefaultSchedule(DayOfWeek dayOfWeek, IList<AvailablePeriod> periods)
    {
        if(Schedules.Any(s => s.Date.DayOfWeek == dayOfWeek))
            ReplaceDefaultSchedule(dayOfWeek, periods);

        var schedules = Schedule.CreateDefault(dayOfWeek, periods);

        foreach (var schedule in schedules)
            this.Schedules.Add(schedule);
    }       

    public void ReplaceDefaultSchedule(DayOfWeek dayOfWeek, IList<AvailablePeriod> periods)
    {
        var schedules = this.Schedules.Except(this.Schedules.Where(s => s.Date.DayOfWeek == dayOfWeek)).ToList();

        var newSchedules = Schedule.CreateDefault(dayOfWeek, periods);

        foreach (var schedule in newSchedules)
            schedules.Add(schedule);

        Schedules = schedules;
    }

    public void ReplaceSchedules(IList<Schedule> schedules)
    {
        foreach (var schedule in schedules)
        {
            if (Schedules.Any(s => s.Date == schedule.Date))
            {
                Schedules.Remove(Schedules.First(s => s.Date == schedule.Date));
                Schedules.Add(schedule);
            }
        }
    }
    public void RemovePastSchedules()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        Schedules = Schedules.Where(schedule => schedule.Date >= today).ToList();
    }

    public void UpdatePhoto(Guid photoId)
    {
        PhotoId = photoId;
    }
}
