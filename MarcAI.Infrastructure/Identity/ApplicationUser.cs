using MarcAI.Domain.Enums.User;
using MarcAI.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MarcAI.Infrastructure.Identity;
public class ApplicationUser : IdentityUser<Guid>
{
    public UserType Role { get; set; }

    public ApplicationUser()
    {
    }

    public static ApplicationUser CreateFromUser(User user)
    {
        return new ApplicationUser { Id = user.Id, Email = user.Email , UserName = user.Email, Role = user.Role};
    }

    public void Update(User user)
    {
        this.UserName = user.Email;
        this.UserName = user.Email;
    }
}
