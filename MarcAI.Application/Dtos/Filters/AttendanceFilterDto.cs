namespace MarcAI.Application.Dtos.Filters;

public class AttendanceFilterDto : FilterDto
{
    public Guid? EmployeeId { get; set; }
    public Guid? CostumerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
