using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnjMedical.Api.Models.Common;
using SnjMedical.Application.Queries.Test;

namespace SnjMedical.Api.Controllers.Test;

/// <summary>
/// Weather forecast controller
/// </summary>
[ApiController]
[Route("api/weatherforecast")]
public class WeatherController : BaseController
{
    private readonly ILogger<WeatherController> _logger;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// method to retrieve collection of weather forecasts
    /// </summary>
    /// <returns></returns>
    [HttpGet("retrieve-weather")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Reply))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonReply))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonReply))]
    public async Task<IActionResult> Get()
    {
        var query = new RetrieveWeatherInfoQuery();
        var reply = await Mediator.Send(query);
        return Ok(reply);
    }
}
