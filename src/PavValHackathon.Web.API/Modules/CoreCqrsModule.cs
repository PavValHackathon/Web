using Autofac;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.v1.Commands.Buckets;
using PavValHackathon.Web.API.v1.Commands.Buckets.Handlers;
using PavValHackathon.Web.API.v1.Commands.Transactions;
using PavValHackathon.Web.API.v1.Commands.Transactions.Decorators;
using PavValHackathon.Web.API.v1.Commands.Transactions.Handlers;
using PavValHackathon.Web.API.v1.Commands.Wallets;
using PavValHackathon.Web.API.v1.Commands.Wallets.Handlers;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.API.v1.Queries.Buckets;
using PavValHackathon.Web.API.v1.Queries.Buckets.Handlers;
using PavValHackathon.Web.API.v1.Queries.Transactions;
using PavValHackathon.Web.API.v1.Queries.Transactions.Handlers;
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
            RegisterTransaction(builder);
            RegisterBucket(builder);
            RegisterWallet(builder);
        }

        private static void RegisterTransaction(ContainerBuilder builder)
        {
            RegisterCommandHandler<CreateTransactionCommand, int, CreateTransactionCommandHandler>(builder)
                .RegisterDecorator<UpdateBucketTransactionCommandDecorator<CreateTransactionCommand, int>>()
                .RegisterDecorator<UpdateWalletTransactionCommandDecorator<CreateTransactionCommand, int>>()
                .RegisterDecorator<TransactionCommandDecorator<CreateTransactionCommand, int>>()
                .InstancePerLifetimeScope();
            
            RegisterQueryHandler<GetTransactionQuery, TransactionDocument, GetTransactionQueryHandler>(builder)
                .InstancePerLifetimeScope();
        }

        private static void RegisterBucket(ContainerBuilder builder)
        {
            RegisterQueryHandler<GetBucketQuery, BucketDocument, GetBucketQueryHandler>(builder)
                .InstancePerLifetimeScope();
            
            RegisterQueryHandler<ListBucketQuery, PaginationCollection<BucketDocument>, ListBucketQueryHandler>(builder)
                .InstancePerLifetimeScope();
            
            RegisterCommandHandler<CreateBucketCommand, int, CreateBucketCommandHandler>(builder)
                .RegisterDecorator<TransactionCommandDecorator<CreateBucketCommand, int>>()
                .InstancePerLifetimeScope();
            
            RegisterCommandHandler<DeleteBucketCommand, DeleteBucketCommandHandler>(builder)
                .RegisterDecorator<TransactionCommandDecorator<DeleteBucketCommand, Void>>()
                .InstancePerLifetimeScope();
            
            RegisterCommandHandler<EditBucketCommand, EditBucketCommandHandler>(builder)
                .RegisterDecorator<TransactionCommandDecorator<EditBucketCommand, Void>>()
                .InstancePerLifetimeScope();
        }

        private static void RegisterWallet(ContainerBuilder builder)
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