using MarcAI.Domain.Enums.User;
using MarcAI.Domain.Models.Common;
using System.Xml.Linq;

namespace MarcAI.Domain.Models.Entities;

public class User : BaseEntity
{
    public new Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserType Role { get; set; }
    public Guid? EmployeeId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? CompanyId { get; set; }


    //Ef Relational
    public Employee? Employee { get; set; }
    public Customer? Customer { get; set; } 
    public Company? Company { get; set; }


    private User() { } // Supressão do aviso de inicialização


    public User(Guid id, string name, string surname, string email, UserType role)
    {
        Id = id;
        Email = email;
        Name = name;
        Surname = surname;
        Role = role;
    }

    public void SetPassword(string password)
    {
        PasswordHash = password;
    }

    public static User Create(string name, string surname, string email, UserType role)
        => new(Guid.NewGuid(),name, surname ,email.ToLower(), role);
}
