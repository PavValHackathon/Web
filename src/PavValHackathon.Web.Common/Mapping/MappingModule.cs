using Autofac;
using PavValHackathon.Web.Common.Mappers.Impl;

namespace PavValHackathon.Web.Common.Mappers
{
    public abstract class MappingModule : Module
    {
        protected sealed override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>()
                .As<IMapper>();
        }
        
        protected abstract void Register(ContainerBuilder builder);
    }
}