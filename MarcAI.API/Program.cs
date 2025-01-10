using FluentValidation.AspNetCore;
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


// Primeiro o autenticao e apenas depois a autorizacao
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services
        .AddIdentityApiEndpoints<ApplicationUser>()
        .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

app.MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
