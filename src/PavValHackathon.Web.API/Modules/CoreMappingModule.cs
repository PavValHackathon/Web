using Autofac;
using PavValHackathon.Web.Common.Mapping;

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