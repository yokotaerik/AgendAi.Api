using MarcAI.Domain.Models.ValueObjects;
using MarcAI.Shared.Dtos.Employees;
using MarcAI.Shared.Dtos.Services;

namespace MarcAI.Shared.Dtos.Companies;

public record CompleteCompanyDto
{
    public Guid? Id { get; init; }
    public string? CorporateName { get; init; }
    public string? FantasyName { get; init; }
    public Address? Address { get; init; }
    public Cnpj? Cnpj { get; init; }
    public required IEnumerable<EmployeeDto> Employees { get; init; }
    public required IEnumerable<ServiceDto> Services { get; init; }
}


