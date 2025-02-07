namespace MarcAI.Shared.Dtos.Common.Filters;

public class FilterDto
{
    public string? Keyword { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
