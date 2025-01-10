using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Entities;

public class Service : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Amount { get; private set; }
    public Guid CompanyId { get; private set; }

    private Service(string name, string? description, decimal amount, Guid companyId)
    {
        Name = name;
        Description = description;
        Amount = amount;
        CompanyId = companyId;
    }
}
