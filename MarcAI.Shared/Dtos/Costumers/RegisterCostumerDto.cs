using MarcAI.Domain.Models.ValueObjects;
using MarcAI.Shared.Dtos.Auth;

namespace MarcAI.Shared.Dtos.Costumers;

public record RegisterCostumerDto
{
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Cpf { get; init; }
    public required LoginDto Credentials { get; init; }
}
