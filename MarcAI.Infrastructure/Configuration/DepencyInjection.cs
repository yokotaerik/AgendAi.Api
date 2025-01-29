using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Infrastructure.Data.Repositories;
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

        return services;
    }
}
