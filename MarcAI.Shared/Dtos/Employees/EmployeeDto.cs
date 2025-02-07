using MarcAI.Domain.Models.Aggregates.Schedules;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Shared.Dtos.Employees;

public record EmployeeDto
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public Cpf? Cpf { get; init; }
    public Guid? UserId { get; init; }
    public Company? Company { get; init; }
    public IList<Schedule>? Schedules { get; init; } = new List<Schedule>();
}
