using Autofac;
using PavValHackathon.Web.Common.Mapping.Impl;

namespace PavValHackathon.Web.Common.Mapping
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