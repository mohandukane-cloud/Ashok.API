using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

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
    }
}