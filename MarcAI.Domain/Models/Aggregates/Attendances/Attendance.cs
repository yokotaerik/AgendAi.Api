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
}
