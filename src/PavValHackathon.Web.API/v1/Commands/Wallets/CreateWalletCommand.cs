using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.v1.Commands.Wallets
{
    public class CreateWalletCommand : ICommand<int>
    {
        public string Title { get; set; } = null!;
        public int CurrencyId { get; set; }
    }
}