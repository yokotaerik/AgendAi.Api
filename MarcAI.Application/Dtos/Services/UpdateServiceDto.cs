using MarcAI.Application.Dtos.Companies;

namespace MarcAI.Application.Dtos.Services;

public record UpdateServiceDto 
{
    public Guid? Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public TimeSpan Duration { get; init; } = default!;
    public decimal Price { get; init; } = default!;
}
