using System;

namespace PavValHackathon.Web.Data.Domain
{
    public class Transaction : IDomainEntity
    {
        public int Id { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public decimal Value { get; set; }
        
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!;
        
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;
        
        public int BucketId { get; set; }
        public Bucket Bucket { get; set; } = null!;
    }
}