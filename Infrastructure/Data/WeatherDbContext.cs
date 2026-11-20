using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Visitor> Visitors => Set<Visitor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>(entity =>
        {
            entity.HasKey(forecast => forecast.Id);
            entity.Property(forecast => forecast.Id).ValueGeneratedOnAdd();
            entity.Property(forecast => forecast.Date).HasColumnType("date");
            entity.Property(forecast => forecast.Summary).IsRequired().HasMaxLength(100);
            entity.Ignore(forecast => forecast.TemperatureF);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id).ValueGeneratedOnAdd();
            entity.Property(user => user.Username).IsRequired().HasMaxLength(100);
            entity.HasIndex(user => user.Username).IsUnique();
            entity.Property(user => user.Email).IsRequired().HasMaxLength(255);
            entity.Property(user => user.PasswordHash).IsRequired().HasMaxLength(500);
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(visitor => visitor.Id);
            entity.Property(visitor => visitor.Id).ValueGeneratedOnAdd();
            entity.Property(visitor => visitor.Name).IsRequired().HasMaxLength(150);
            entity.Property(visitor => visitor.Email).IsRequired().HasMaxLength(255);
            entity.Property(visitor => visitor.Purpose).IsRequired().HasMaxLength(500);
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(visitor => visitor.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}