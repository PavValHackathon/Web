using Autofac;
using Microsoft.EntityFrameworkCore;
using PavValHackathon.Web.Data.Contexts;

namespace PavValHackathon.Web.Data
{
    public abstract class DataModule : Module
    {
        protected sealed override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>().As<DbContext>().InstancePerLifetimeScope();
            
            Register(builder);
        }

        protected abstract void Register(ContainerBuilder builder);
    }
}