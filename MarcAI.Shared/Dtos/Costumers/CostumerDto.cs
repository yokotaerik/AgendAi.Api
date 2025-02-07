using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Shared.Dtos.Costumers;

public record CostumerDto
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public Cpf? Cpf { get; init; }
    public Guid? UserId { get; init; }
}
