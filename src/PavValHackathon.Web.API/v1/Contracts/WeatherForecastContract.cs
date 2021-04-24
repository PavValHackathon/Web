using System;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts
{
    [SwaggerDiscriminator("WeatherForecast")]
    public record WeatherForecastContract
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

        public string Summary { get; set; } = string.Empty;
    }
}