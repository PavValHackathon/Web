using Autofac;

namespace PavValHackathon.Web.Data
{
    public abstract class DataModule : Module
    {
        protected sealed override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }

        protected abstract void Register(ContainerBuilder builder);
    }
}