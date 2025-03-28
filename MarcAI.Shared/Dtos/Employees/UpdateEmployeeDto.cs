﻿using MarcAI.Domain.Models.Aggregates.Schedules;

namespace MarcAI.Shared.Dtos.Employees;

public record UpdateEmployeeDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
}
