using MarcAI.Domain.Models.Aggregates.Attendances;
using MarcAI.Domain.Models.Aggregates.Schedules;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;
using MarcAI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Infrastructure.Data.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Company> companies { get; set; }
    public DbSet<Customer> customers { get; set; }
    public DbSet<Employee> employees { get; set; }
    public DbSet<Service> services { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<Attendance> attendances { get; set; }
    public DbSet<AttendenceService> attendancesServices { get; set; }
    public DbSet<AvailablePeriod> availablePeriods { get; set; }
    public DbSet<Schedule> schedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações adicionais de mapeamento podem ser feitas aqui
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<Company>()
             .OwnsOne(c => c.Cnpj, cnpj =>
             {
                 cnpj.Property(c => c.Value).HasColumnName("Cnpj");
             });

        modelBuilder.Entity<Company>()
            .OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("Street");
                address.Property(a => a.Number).HasColumnName("Number");
                address.Property(a => a.Complement).HasColumnName("Complement");
                address.Property(a => a.Neighborhood).HasColumnName("Neighborhood");
                address.Property(a => a.City).HasColumnName("City");
                address.Property(a => a.State).HasColumnName("State");
                address.Property(a => a.ZipCode).HasColumnName("ZipCode");
            });

        modelBuilder.Entity<Customer>()
            .OwnsOne(c => c.Cpf, cpf =>
            {
                cpf.Property(c => c.Value).HasColumnName("Cpf");
            });

        modelBuilder.Entity<Employee>()
            .OwnsOne(e => e.Cpf, cpf =>
            {
                cpf.Property(c => c.Value).HasColumnName("Cpf");
            });

        modelBuilder.Entity<AttendenceService>()
            .HasKey(x => new { x.AttendenceId, x.ServiceId });

  

    }
}
