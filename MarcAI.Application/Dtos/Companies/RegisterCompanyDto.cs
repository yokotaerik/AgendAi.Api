using MarcAI.Application.Dtos.Common;

namespace MarcAI.Application.Dtos.Companies;

public record RegisterCompanyDto 
{
    public string? CorporateName { get; init; }
    public string? FantasyName { get; init; }
    public required AddressDto Address { get; init; } 
    public string? Cnpj { get; init; }
}
