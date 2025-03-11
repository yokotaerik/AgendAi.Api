using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Application.Dtos.Companies;

public record UpdateCompanyDto 
{
    public Guid? Id { get; init; }
    public string? CorporateName { get; init; }
    public string? FantasyName { get; init; }
    public List<string>? Emails { get; init; }
    public List<string>? Phones { get; init; }
    public Address? Address { get; init; }
}
