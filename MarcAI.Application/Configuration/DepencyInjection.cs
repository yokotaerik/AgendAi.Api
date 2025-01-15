using MarcAI.Application.Interfaces;
using MarcAI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarcAI.Application.Configuration;

public static class DepencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CostumerService>();

        return services;
    }
}
