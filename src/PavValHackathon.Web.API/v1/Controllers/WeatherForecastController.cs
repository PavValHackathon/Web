using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.API.v1.Queries;
using PavValHackathon.Web.Common.Cqrs.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IQueryExecutor _queryExecutor;

        public WeatherForecastController(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(WeatherForecastContract))]
        public async Task<IActionResult> Get()
        {
            var query = new ListWeatherForecastQuery();
            var res = await _queryExecutor.ExecuteAsync<ListWeatherForecastQuery, List<WeatherForecastContract>>(query);
            
            return Ok(res);
        }
    }
}