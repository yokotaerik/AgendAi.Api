using MarcAI.Domain.Enums.User;
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

    public async Task<User> Create(User user, string password)
    {
        await _dbSet.AddAsync(user);

        return user;
    }

    public async Task<User> Update(User user)
    {
        var identityUser = await _userManager.FindByEmailAsync(user.Email) ?? throw new Exception("User not found");
        
        identityUser.Update(user);

        try
        {
        await _userManager.UpdateAsync(identityUser);

        } catch(Exception ex)
        {

        }

        _dbSet.Update(user);

        return user;
    }

    public Task<User?> GetById(Guid id)
    {
        return _dbSet.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<string?> GetSenderName(Guid id)
    {
        return _dbSet
        .Where(u => u.Id == id)
        .Select(u => u.Role == UserType.Employee
            ? (u.Employee!.Owner ? u.Employee.Company.FantasyName : u.Name + " " + u.Surname)
            : u.Name + " " + u.Surname)
        .FirstOrDefaultAsync();
    }
}
