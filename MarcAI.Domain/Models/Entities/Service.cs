using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Entities;

public class Service : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public TimeSpan Duration { get; private set; }
    public Guid CompanyId { get; private set; }


    #pragma warning disable CS8618
        private Service() { } // Supressão do aviso de inicialização
    #pragma warning restore CS8618

    public static Service Create(string name, string? description, TimeSpan duration, decimal amount, Guid companyId)
    {
        return new Service(name, description,duration, amount, companyId);
    }

    public void Update(string name, string? description, TimeSpan duration, decimal amount)
    {
        Name = name;
        Description = description;
        Duration = duration;
        Price = amount;
    }


    private Service(string name, string? description, TimeSpan duration, decimal amount, Guid companyId)
    {
        Name = name;
        Description = description;
        Duration = duration;
        Price = amount;
        CompanyId = companyId;
    }
}
