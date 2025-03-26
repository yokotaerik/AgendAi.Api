using MarcAI.Domain.Models.Aggregates.Attendances;
using MarcAI.Domain.Models.Aggregates.Schedules;
using MarcAI.Domain.Models.Common;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;
using MarcAI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
    public DbSet<Photo> photos { get; set; }
    public DbSet<Message> messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var filter = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(BaseEntity.IsDeleted)),
                        Expression.Constant(false)
                    ),
                    parameter
                );
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }

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
             .OwnsMany(c => c.Contacts, contact =>
             {
                 contact.Property(c => c.Info).HasColumnName("Info");
                 contact.Property(contact => contact.Type).HasColumnName("Type");
             });

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Photos);

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
            })
            .HasOne(e => e.Photo)
            .WithOne()
            .HasForeignKey<Photo>(e => e.EmployeeId);

        modelBuilder.Entity<Employee>()
        .HasOne(e => e.User)
        .WithOne()
        .HasForeignKey<Employee>(e => e.UserId);

        modelBuilder.Entity<AttendenceService>()
            .HasKey(x => new { x.AttendenceId, x.ServiceId });


        modelBuilder.Entity<User>()
        .HasOne(u => u.Employee)
        .WithOne(e => e.User)
        .HasForeignKey<Employee>(e => e.UserId)
        .IsRequired(false); // Indica que o relacionamento é opcional

        modelBuilder.Entity<User>()
        .HasOne(u => u.Customer)
        .WithOne(e => e.User)
        .HasForeignKey<Customer>(e => e.UserId)
        .IsRequired(false); // Indica que o relacionamento é opcional

        modelBuilder.Entity<Message>()
            .HasIndex(e => e.Id);
        modelBuilder.Entity<Message>()
           .HasIndex(e => e.SenderId);
        modelBuilder.Entity<Message>()
           .HasIndex(e => e.ReceiverId);
    }
}
