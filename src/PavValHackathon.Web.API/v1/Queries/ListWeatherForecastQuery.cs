using System.Collections.Generic;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common.Cqrs.Queries;

namespace PavValHackathon.Web.API.v1.Queries
{
    public record ListWeatherForecastQuery : IQuery<List<WeatherForecastContract>>
    {
        
    }
}