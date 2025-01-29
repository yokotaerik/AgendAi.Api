using MarcAI.Domain.Models.Common;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Domain.Models.Entities;

public class Customer : BaseEntity
{
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public Cpf Cpf { get; private set; }
    public Guid UserId { get; private set; }

    //Ef Relational
    public User User { get; private set; } = null!;

#pragma warning disable CS8618
    private Customer() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618

    private Customer(string name, string surname, Cpf cpf, Guid userId)
    {
        Name = name;
        Surname = surname;
        Cpf = cpf;
        UserId = userId;
    }

    public static Customer Create(string name, string surname, string cpf, Guid userId)
    {
        return new Customer(name, surname, Cpf.Create(cpf), userId);
    }

}
