using MarcAI.Domain.Models.Common;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Domain.Models.Entities;

public class Customer : BaseEntity
{
    public Cpf? Cpf { get; private set; }
    public Guid UserId { get; private set; }

    //Ef Relational
    public User User { get; private set; } = null!;

#pragma warning disable CS8618
    private Customer() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618

    private Customer(Cpf? cpf, Guid userId)
    {
        Cpf = cpf;
        UserId = userId;
    }

    public static Customer Create(Guid userId)
    {
        return new Customer(null, userId);
    }

}
