using MarcAI.Application.Dtos.Auth;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Application.Dtos.Costumers;

public record RegisterCostumerDto 
{
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Cpf { get; init; }
    public required LoginDto Credentials { get; init; } 
}
