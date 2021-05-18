using System.Collections.Generic;
using Autofac;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.v1.Commands.Wallets;
using PavValHackathon.Web.API.v1.Commands.Wallets.Handlers;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.API.v1.Queries.Wallets;
using PavValHackathon.Web.API.v1.Queries.Wallets.Handlers;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs;
using PavValHackathon.Web.Data.Decorators;

namespace PavValHackathon.Web.API.Modules
{
    public class CoreCqrsModule : CqrsModule
    {
        protected override void Register(ContainerBuilder builder)
        {   
            RegisterCommandHandler<DeleteWalletCommand, DeleteWalletCommandHandler>(builder)
                .RegisterDecorator<TransactionCommandDecorator<DeleteWalletCommand, Void>>()
                .InstancePerLifetimeScope();
            
            RegisterCommandHandler<CreateWalletCommand, int, CreateWalletCommandHandler>(builder)
                .RegisterDecorator<TransactionCommandDecorator<CreateWalletCommand, int>>()
                .InstancePerLifetimeScope();
            
            RegisterCommandHandler<EditWalletCommand, EditWalletCommandHandler>(builder)
                .RegisterDecorator<TransactionCommandDecorator<EditWalletCommand, Void>>()
                .InstancePerLifetimeScope();
            
            RegisterQueryHandler<GetWalletQuery, WalletDocument, GetWalletQueryHandler>(builder)
                .InstancePerLifetimeScope();
            
            RegisterQueryHandler<ListWalletQuery, PaginationCollection<WalletDocument>, ListWalletQueryHandler>(builder)
                .InstancePerLifetimeScope();
        }
    }
}