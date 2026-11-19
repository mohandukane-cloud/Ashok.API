using Domain.Entities;

namespace Application.Interfaces;

public interface IWeatherForecastRepository
{
    Task<WeatherForecast> CreateAsync(WeatherForecast forecast, CancellationToken cancellationToken);

    Task<IReadOnlyList<WeatherForecast>> GetForecastAsync(CancellationToken cancellationToken);
}
