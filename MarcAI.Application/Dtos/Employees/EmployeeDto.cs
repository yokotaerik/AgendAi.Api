using MarcAI.Application.Dtos.Companies;
using MarcAI.Application.Dtos.Schedules;
using MarcAI.Application.Dtos.Services;

namespace MarcAI.Application.Dtos.Employees;

public record EmployeeDto
{
    public Guid? Id { get; init; }
    public string? ImageUrl { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public bool Owner { get; init; }
    public Guid? UserId { get; init; }
    public CompanyDto? Company { get; init; }
    public IList<ServiceDto> Services { get; init; } = [];
    public IList<ScheduleDto>? Schedules { get; init; } = [];
}