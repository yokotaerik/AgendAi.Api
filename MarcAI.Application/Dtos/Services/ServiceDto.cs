﻿namespace MarcAI.Application.Dtos.Services;

public record ServiceDto
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public TimeSpan? Duration { get; init; }

    public Guid? CompanyId { get; init; }
}
