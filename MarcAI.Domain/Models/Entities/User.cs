using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Entities;

public class User : BaseEntity
{
    public new Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

#pragma warning disable CS8618
        private User() { } // Supressão do aviso de inicialização
#pragma warning restore CS8618


    public User(Guid id, string email)
    {
        Id = id;
        Email = email;
    }

    public void SetPassword(string password)
    {
        PasswordHash = password;
    }

    public static User Create(string email )
        => new User(Guid.NewGuid(), email);
}
