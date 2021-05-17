using System;
using Autofac;
using PavValHackathon.Web.Common.Mapping.Impl;
using Module = Autofac.Module;

namespace PavValHackathon.Web.Common.Mapping
{
    public abstract class MappingModule : Module
    {
        protected sealed override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>()
                .As<IMapper>();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IMapperDefinition<,>))
                .AsImplementedInterfaces();
        }
        
        protected abstract void Register(ContainerBuilder builder);
    }
}