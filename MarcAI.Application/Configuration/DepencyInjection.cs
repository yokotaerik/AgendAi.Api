using MarcAI.Application.Interfaces;
using MarcAI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarcAI.Application.Configuration;

public static class DepencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CostumerService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IUserService, UserService>(); 
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }
}
