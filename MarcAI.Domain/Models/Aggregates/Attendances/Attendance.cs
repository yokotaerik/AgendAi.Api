using MarcAI.Domain.Models.Common;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Models.Aggregates.Attendances;

public class Attendance : BaseEntity
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public TimeSpan Duration { get; private set; }
    public decimal TotalValue { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; } = null!;

    public Guid CostumerId { get; private set; }
    public Customer Costumer { get; private set; } = null!;

    public IList<AttendenceService> Services { get; private set; } = new List<AttendenceService>();


#pragma warning disable CS8618
    private Attendance() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618



    private Attendance(DateTime start,
                       Guid employeeId,
                       Guid costumerId,
                       TimeSpan duration,
                       decimal totalValue)
    {
        Start = start;
        End = start + duration;
        Duration = duration;
        TotalValue = 0;
        EmployeeId = employeeId;
        CostumerId = costumerId;
        TotalValue = totalValue;
    }

    public static Attendance Create(DateTime start,
                                    Guid employeeId,
                                    Guid costumerId,
                                    TimeSpan duration,
                                    decimal totalValue)
    {
        return new Attendance(start, employeeId, costumerId, duration, totalValue);
    }

    public void AddService(Service service)
    {
        Services.Add(AttendenceService.Create(Id, service.Id, service.Price));
        TotalValue += service.Price;
    }

    public void AddServices(IEnumerable<Service> services)
    {
        Services = AttendenceService.Create(Id, services);
        TotalValue = services.Sum(service => service.Price);
    }
}
