namespace Domain.Entities;

/// <summary>Represents a daily weather forecast in the domain.</summary>
public sealed record WeatherForecast(
    DateOnly Date,
    int TemperatureC,
    string Summary)
{
    public int Id { get; private set; }

    /// <summary>Gets the temperature in degrees Fahrenheit.</summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
