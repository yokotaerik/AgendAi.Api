using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces;

public interface IUserService
{
    Task<User> GetCurrentUserAsync();
    Task<Employee> GetCurrentEmployeIncludeUser();
    Guid GetUserId();
}
