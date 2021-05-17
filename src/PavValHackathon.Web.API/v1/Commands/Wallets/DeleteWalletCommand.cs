using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.v1.Commands.Wallets
{
    public class DeleteWalletCommand : ICommand<Void>
    {
        public DeleteWalletCommand(int id)
        {
            Id = id;
        }
        
        public int Id { get; }
    }
}