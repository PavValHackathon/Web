using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Queries;
using PavValHackathon.Web.Common.Extensions;

namespace PavValHackathon.Web.API.v1.Queries.Handlers
{
    public class ListWeatherForecastQueryHandler : IQueryHandler<ListWeatherForecastQuery, List<WeatherForecastContract>>
    {
        private static readonly string[] Summaries = 
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        public Task<Result<List<WeatherForecastContract>>> HandleAsync(ListWeatherForecastQuery query, CancellationToken cancellationToken = default)
        {
            var rng = new Random();
            var res = Enumerable.Range(1, 5).Select(index => new WeatherForecastContract
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();

            return Result.Ok(res).AsAsync();
        }
    }
}