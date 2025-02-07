using MarcAI.Application.Dtos.Common.User;

namespace MarcAI.Application.Dtos.Attendances;

public class AttendanceDto
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public required BasicInfoDto Costumer { get; set; }
    public required BasicInfoDto Employee { get; set; }
}
