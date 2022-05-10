using SnjMedical.Api.Models.Common;

namespace SnjMedical.Api.Models.Test.Dtos
{
    /// <summary>
    /// Weather forecast model
    /// </summary>
    public class WeatherForecast : CommonReply
    {
        /// <summary>
        /// current date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// temperature in celcius
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// temperature in farenheit
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// summary information
        /// </summary>
        public string? Summary { get; set; }
    }
}
