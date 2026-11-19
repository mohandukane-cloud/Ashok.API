using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class EfWeatherForecastRepository(WeatherDbContext dbContext) : IWeatherForecastRepository
{
    public async Task<WeatherForecast> CreateAsync(
        WeatherForecast forecast,
        CancellationToken cancellationToken)
    {
        dbContext.WeatherForecasts.Add(forecast);
        await dbContext.SaveChangesAsync(cancellationToken);
        return forecast;
    }

    public async Task<IReadOnlyList<WeatherForecast>> GetForecastAsync(
        CancellationToken cancellationToken)
    {
        return await dbContext.WeatherForecasts
            .AsNoTracking()
            .OrderBy(forecast => forecast.Date)
            .ToArrayAsync(cancellationToken);
    }
}