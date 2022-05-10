using SnjMedical.Domain.Models;

namespace SnjMedical.Application.Interfaces.Test;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherInfoAsync();
}
