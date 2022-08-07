using System.Collections.Generic;

namespace PavValHackathon.Web.Data.Domain
{
    public class Wallet : IDomainEntity
    {
        public int Id { get; set; }
        
        public decimal Amount { get; set; }

        public string Title { get; set; } = string.Empty;
        
        public int UserId { get; set; }
        
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;

        public List<Transaction> Transactions { get; set; } = new();
    }
}