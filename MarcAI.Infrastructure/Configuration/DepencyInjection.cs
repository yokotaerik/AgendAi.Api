using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Interfaces.Services;
using MarcAI.Infrastructure.Data.Repositories;
using MarcAI.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MarcAI.Infrastructure.Configuration;

public static class DepencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICostumerRepository, CustomerRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
