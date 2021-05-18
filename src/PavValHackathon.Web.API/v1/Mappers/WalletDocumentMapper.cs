using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class WalletDocumentMapper : IMapperDefinition<Wallet, WalletDocument>
    {
        public WalletDocument Map(Wallet wallet)
        {
            Assert.IsNotNull(wallet, nameof(wallet));

            return Map(new WalletDocument(), wallet);
        }

        public WalletDocument Map(WalletDocument walletDocument, Wallet wallet)
        {
            Assert.IsNotNull(walletDocument, nameof(walletDocument));
            Assert.IsNotNull(wallet, nameof(wallet));

            walletDocument.Id = wallet.Id;
            walletDocument.Title = wallet.Title;
            walletDocument.Amount = wallet.Amount;
            walletDocument.CurrencyId = wallet.CurrencyId;
            walletDocument.Currency = wallet.Currency.Name;
            
            return walletDocument;
        }
    }
}