using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Application.Dtos.Employees;

public record RegisterEmployeeDto
{
    public string Name { get; init; } = default!;
    public string Surname { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
