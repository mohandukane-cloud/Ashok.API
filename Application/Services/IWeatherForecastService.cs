using Application.Models;

namespace Application.Services;

public interface IWeatherForecastService
{
    Task<WeatherForecastResponse> CreateForecastAsync(
        DateOnly date,
        int temperatureC,
        string summary,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<WeatherForecastResponse>> GetForecastAsync(CancellationToken cancellationToken);
}
