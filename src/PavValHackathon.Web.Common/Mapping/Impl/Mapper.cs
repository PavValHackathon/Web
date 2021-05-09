using System;
using Autofac;

namespace PavValHackathon.Web.Common.Mapping.Impl
{
    public class Mapper : IMapper
    {
        private readonly ILifetimeScope _lifetimeScope;

        public Mapper(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public TTo MapFrom<TFrom, TTo>(TFrom from)
        {
            return GetMapperDefinition<TFrom, TTo>().Map(from);
        }

        public TTo MapFrom<TFrom, TTo>(TTo to, TFrom from)
        {
            return GetMapperDefinition<TFrom, TTo>().Map(to, from);
        }
        
        private IMapperDefinition<TFrom, TTo> GetMapperDefinition<TFrom, TTo>()
        {
            return _lifetimeScope.Resolve<IMapperDefinition<TFrom, TTo>>()
                ?? throw new InvalidOperationException($"Could not find mapper for type {typeof(TFrom).Name}.");
        }
    }
}