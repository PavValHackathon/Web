using Autofac;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.API.v1.Mappers;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.API.Modules
{
    public class CoreMappingModule : MappingModule
    {
        protected override void Register(ContainerBuilder builder)
        {
            //builder.RegisterType<WalletDocumentMapper>().As<IMapperDefinition<Wallet, WalletDocument>>();
        }
    }
}