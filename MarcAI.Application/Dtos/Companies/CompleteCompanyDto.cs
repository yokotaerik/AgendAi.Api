using MarcAI.Application.Dtos.Common.User;
using MarcAI.Application.Dtos.Employees;
using MarcAI.Application.Dtos.Services;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Application.Dtos.Companies;

public record CompleteCompanyDto
{
    public Guid? Id { get; init; }
    public string? CorporateName { get; init; }
    public string? FantasyName { get; init; }
    public Address? Address { get; init; }
    public required IEnumerable<BasicInfoDto> Employees { get; init; }
    public required IEnumerable<ServiceDto> Services { get; init; }
    public List<string> ImageUrls { get; init; } = [];
}


