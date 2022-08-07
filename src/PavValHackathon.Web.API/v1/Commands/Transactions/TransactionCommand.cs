using System;
using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.v1.Commands.Transactions
{
    public class TransactionCommand<TResult> : ICommand<TResult>
    {
        public decimal Value { get; set; }
        public DateTime DateTime { get; set; }
        public int CurrencyId { get; set; }
        public int? WalletId { get; set; }
        public int BucketId { get; set; }
    }
}