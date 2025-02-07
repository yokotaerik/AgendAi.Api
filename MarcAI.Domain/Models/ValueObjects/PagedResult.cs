namespace MarcAI.Domain.Models.ValueObjects;

public record PagedResult<T>(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
{
    public int TotalPages => (int)Math.Ceiling((double)totalCount / pageSize);
}
