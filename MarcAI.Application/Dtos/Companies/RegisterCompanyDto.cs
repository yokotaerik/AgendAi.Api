using MarcAI.Application.Dtos.Common;
using MarcAI.Application.Dtos.Employees;

namespace MarcAI.Application.Dtos.Companies;

public record RegisterCompanyDto 
{
    public string? CorporateName { get; init; }
    public string? FantasyName { get; init; }
    public required AddressDto Address { get; init; } 
    public required RegisterEmployeeDto Owner { get; init; }
}
