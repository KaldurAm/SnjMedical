using Microsoft.Extensions.Logging;

using SnjMedical.Application.Interfaces.Test;
using SnjMedical.Domain.Models;

namespace SnjMedical.Infrastructure.Services.Test;

public class WeatherService : IWeatherService
{
    private readonly ILogger<WeatherService> _logger;
    private readonly string[] _summaries;

    public WeatherService(ILogger<WeatherService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(WeatherService));
        _summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching",
        };
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherInfoAsync()
    {
        await Task.Delay(5000);
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = _summaries[rng.Next(_summaries.Length)]
        }).ToArray();
    }
}
