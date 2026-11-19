using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public sealed class WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
    : IWeatherForecastService
{
    public async Task<WeatherForecastResponse> CreateForecastAsync(
        DateOnly date,
        int temperatureC,
        string summary,
        CancellationToken cancellationToken)
    {
        var forecast = await weatherForecastRepository.CreateAsync(
            new Domain.Entities.WeatherForecast(date, temperatureC, summary),
            cancellationToken);

        return ToResponse(forecast);
    }

    public async Task<IReadOnlyList<WeatherForecastResponse>> GetForecastAsync(
        CancellationToken cancellationToken)
    {
        var forecasts = await weatherForecastRepository.GetForecastAsync(cancellationToken);

        return forecasts
            .Select(ToResponse)
            .ToArray();
    }

    private static WeatherForecastResponse ToResponse(Domain.Entities.WeatherForecast forecast) =>
        new(forecast.Date, forecast.TemperatureC, forecast.Summary, forecast.TemperatureF);
}
