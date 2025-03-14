using MarcAI.Application.Dtos.Companies;
using MarcAI.Application.Dtos.Schedules;
using MarcAI.Domain.Models.Aggregates.Schedules;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Application.Dtos.Employees;

public record EmployeeDto
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public Cpf? Cpf { get; init; }
    public bool Owner { get; init; }
    public Guid? UserId { get; init; }
    public CompanyDto? Company { get; init; }
    public IList<ScheduleDto>? Schedules { get; init; } = new List<ScheduleDto>();
}
