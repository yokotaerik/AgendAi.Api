using MarcAI.Domain.Interfaces;
using MarcAI.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MarcAI.Infrastructure.Identity;

internal class UserManagerService : IUserManager
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserManagerService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<bool> CreateUserAsync(User user, string password)
    {
        var identityUser = ApplicationUser.CreateFromUser(user);

        var sucess = await _userManager.CreateAsync(identityUser, password);

        if (!sucess.Succeeded)
        {
            var errors = string.Join(", ", sucess.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        return true;
    }
}
