using Autofac;
using Microsoft.EntityFrameworkCore;
using PavValHackathon.Web.Data.Contexts;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;
using PavValHackathon.Web.Data.Repositories.Custom;
using PavValHackathon.Web.Data.Repositories.Custom.Impl;

namespace PavValHackathon.Web.Data
{
    public abstract class DataModule : Module
    {
        protected sealed override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>().As<DbContext>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ReadOnlyRepository<>))
                .As(typeof(IReadOnlyRepository<>))
                .InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<TransactionRepository>()
                .As<IReadOnlyRepository<Transaction>>()
                .As<IRepository<Transaction>>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<BucketRepository>()
                .As<IReadOnlyRepository<Bucket>>()
                .As<IRepository<Bucket>>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<WalletRepository>()
                .As<IReadOnlyRepository<Wallet>>()
                .As<IRepository<Wallet>>()
                .As<IWalletRepository>()
                .As<IWalletReadOnlyRepository>()
                .InstancePerLifetimeScope();
            
            Register(builder);
        }

        protected abstract void Register(ContainerBuilder builder);
    }
}