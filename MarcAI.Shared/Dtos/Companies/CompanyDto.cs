using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Shared.Dtos.Companies;

public record CompanyDto
{
    public Guid? Id { get; init; }
    public string? CorporateName { get; init; }
    public string? FantasyName { get; init; }
    public Address? Address { get; init; }
    public Cnpj? Cnpj { get; init; }
}
