namespace MarcAI.Application.Dtos.Services;

public record RegisterServiceDto 
{
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public decimal Price { get; init; } = default!;
    public TimeSpan Duration { get; init; } = default!;
    public Guid CompanyId { get; init; } 
}
