using FluentValidation.AspNetCore;
using MarcAI.Application.Configuration;
using MarcAI.Application.Mapping;
using MarcAI.Infrastructure.Configuration;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation(fv =>
{
    // Desativa validações do DataAnnotations para evitar duplicidade
    fv.DisableDataAnnotationsValidation = true;
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Primeiro o autenticao e apenas depois a autorizacao
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Dependency injection 

// Application
builder.Services.AddApplicationServices();

// Infra
builder.Services.AddRepositories();


builder.Services
        .AddIdentityApiEndpoints<ApplicationUser>()
        .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAutoMapper(typeof(StartupBase));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors();

app.UseHttpsRedirection();

app.Run();
