using MarcAI.Domain.Models.Entities;
namespace MarcAI.Domain.Models.Aggregates.Attendances;

public class AttendenceService
{
    public Guid AttendenceId { get; private set; }
    public Attendance Attendance { get; private set; } = null!;
    public Guid ServiceId { get; private set; }
    public Service Service { get; private set; } = null!;
    public decimal Value { get; private set; }

#pragma warning disable CS8618
    private AttendenceService() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618

    private AttendenceService(Guid attendenceId, Guid serviceId, decimal value)
    {
        AttendenceId = attendenceId;
        ServiceId = serviceId;
        Value = value;
    }

    public static AttendenceService Create(Guid attendenceId, Guid serviceId, decimal value)
    {
        return new AttendenceService(attendenceId, serviceId, value);
    }

    public static List<AttendenceService> Create(Guid attendenceId, IEnumerable<Service> services)
    {
        return services.Select(service => new AttendenceService(attendenceId, service.Id, service.Price)).ToList();
    }
}
