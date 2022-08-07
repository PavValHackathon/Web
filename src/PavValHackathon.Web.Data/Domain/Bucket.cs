using System.Collections.Generic;

namespace PavValHackathon.Web.Data.Domain
{
    public class Bucket : IDomainEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        
        public decimal Amount { get; set; }
        
        public int PictureId { get; set; }
        
        public int UserId { get; set; }
        
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;

        public List<Transaction> Transactions { get; set; } = null!;
    }
}