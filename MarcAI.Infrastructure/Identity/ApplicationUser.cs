
using MarcAI.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MarcAI.Infrastructure.Identity;
public class ApplicationUser : IdentityUser<Guid>
{
    public User ToDomainUser()
    {
        return new User
        (
            Id,
            Email!,
            string.Empty
        );
    }

    public ApplicationUser()
    {
    }

    public static ApplicationUser CreateFromUser(User user)
    {
        return new ApplicationUser { Id = user.Id, Email = user.Email , UserName = user.Email, PasswordHash = user.HashedPassword};
    } 
}
