
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
}
