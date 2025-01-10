using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Entities;

public class User : BaseEntity
{
    public new Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;

    public User(Guid id, string email, string hashedPassword)
    {
        Id = id;
        Email = email;
        HashedPassword = hashedPassword;
    }

    public static User Create(string email, string hashedPassword)
        => new User(Guid.NewGuid(), email, hashedPassword);
}
