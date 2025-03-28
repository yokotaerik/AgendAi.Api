﻿using MarcAI.Domain.Interfaces;
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
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserManager, UserManagerService>();

        return services;
    }
}
