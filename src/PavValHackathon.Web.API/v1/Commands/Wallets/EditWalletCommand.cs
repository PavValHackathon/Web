using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.v1.Commands.Wallets
{
    public class EditWalletCommand : ICommand<Void>
    {
        public EditWalletCommand(int id)
        {
            Id = id;
        }
        
        public int Id { get; set; } 
        
        public string? Title { get; set; }
        public int? CurrencyId { get; set; }
    }
}