using MarcAI.Domain.Models.Aggregates.Schedules;
using MarcAI.Domain.Models.Common;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Domain.Models.Entities;
public class Employee : BaseEntity
{
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public Cpf Cpf { get; private set; }
    public Guid UserId { get; private set; }
    public Company Company {  get; private set; }
    public bool Owner { get; private set; }
    public IList<Schedule> Schedules { get; private set; } = new List<Schedule>();
    private Employee(string name, string surname, Cpf cpf, Guid userId, Company company, bool owner)
    {
        Name = name;
        Surname = surname;
        Cpf = cpf;
        UserId = userId;
        Company = company;
        Owner = owner;
    }

    public static Employee Create(string name, string surname, Cpf cpf, Guid userId, Company company, bool owner)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Surname cannot be empty");
        if (cpf == null) throw new ArgumentNullException(nameof(cpf));
        if (company == null) throw new ArgumentNullException(nameof(company));

        return new Employee(name, surname, cpf, userId, company, owner);
    }

    public void Update(string? name, string? surname)
    {
        if (name != null) UpdateName(name);
        if (surname != null) UpdateSurname(surname);
    }

    public void UpdateName(string? name) {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        Name = name;
    }

    public void UpdateSurname(string? surname)
    {
        if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Surname cannot be empty");
        Surname = surname;
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
}
