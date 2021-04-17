using System.Collections.Generic;
using Autofac;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.API.v1.Queries;
using PavValHackathon.Web.API.v1.Queries.Decorators;
using PavValHackathon.Web.API.v1.Queries.Handlers;
using PavValHackathon.Web.Common.Cqrs;

namespace PavValHackathon.Web.API.Modules
{
    public class CoreCqrsModule : CqrsModule
    {
        protected override void Register(ContainerBuilder builder)
        {
            RegisterQueryHandler<ListWeatherForecastQuery, List<WeatherForecastContract>, ListWeatherForecastQueryHandler>(builder)
                .RegisterDecorator<QueryCachingDecorator<ListWeatherForecastQuery, List<WeatherForecastContract>>>()
                .InstancePerLifetimeScope();
        }
    }
}