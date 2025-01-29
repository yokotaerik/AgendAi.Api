using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Data.UoW;
using MarcAI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Infrastructure.Data.Repositories;

internal class UserRepository : UnitOfWork, IUserRepository
{
    private readonly DbSet<User> _dbSet;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager) : base(context)
    {
        _dbSet = _context.Set<User>();
        _userManager = userManager;
    }

    public async Task<User> Create(User user)
    {
        var identityUser = ApplicationUser.CreateFromUser(user);

        if (!_userManager.CreateAsync(identityUser).Result.Succeeded)
        {
            var errors = string.Join(", ", _userManager.CreateAsync(identityUser).Result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        await _dbSet.AddAsync(user);

        return user;
    }

    public Task<User?> GetById(Guid id)
    {
        return _dbSet.FirstOrDefaultAsync(u => u.Id == id);
    }
}
