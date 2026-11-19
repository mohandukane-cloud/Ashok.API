namespace Application.Models;

public sealed record CreateWeatherForecastRequest(
    DateOnly Date,
    int TemperatureC,
    string Summary);