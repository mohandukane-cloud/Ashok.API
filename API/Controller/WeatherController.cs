using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("weatherforecast")]
public sealed class WeatherController(IWeatherForecastService weatherForecastService) : ControllerBase
{
    /// <summary>Returns all weather forecasts stored in the database.</summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<WeatherForecastResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<WeatherForecastResponse>>> Get(
        CancellationToken cancellationToken)
    {
        var forecasts = await weatherForecastService.GetForecastAsync(cancellationToken);
        return Ok(forecasts);
    }

    [HttpPost]
    [ProducesResponseType<WeatherForecastResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WeatherForecastResponse>> Create(
        CreateWeatherForecastRequest request,
        CancellationToken cancellationToken)
    {
        var forecast = await weatherForecastService.CreateForecastAsync(
            request.Date,
            request.TemperatureC,
            request.Summary,
            cancellationToken);

        return CreatedAtAction(nameof(Get), null, forecast);
    }
}