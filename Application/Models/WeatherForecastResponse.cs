namespace Application.Models;

/// <summary>Represents weather forecast data returned by the API.</summary>
public sealed record WeatherForecastResponse(
    DateOnly Date,
    int TemperatureC,
    string Summary,
    int TemperatureF);
