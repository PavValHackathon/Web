using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bucket>(BuildBucketType);
            modelBuilder.Entity<Wallet>(BuildWalletType);
            modelBuilder.Entity<Currency>(BuildCurrencyType);
            modelBuilder.Entity<Transaction>(BuildTransactionType);
        }

        private static void BuildTransactionType(EntityTypeBuilder<Transaction> entityBuilder)
        {
            entityBuilder.HasKey(transaction => transaction.Id);

            entityBuilder.HasOne(transaction => transaction.Bucket)
                .WithMany(bucket => bucket.Transactions)
                .HasForeignKey(transaction => transaction.BucketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
            entityBuilder.HasOne(transaction => transaction.Wallet)
                .WithMany(wallet => wallet.Transactions)
                .HasForeignKey(transaction => transaction.WalletId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
            entityBuilder.HasOne(transaction => transaction.Currency)
                .WithMany()
                .HasForeignKey(transaction => transaction.CurrencyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void BuildBucketType(EntityTypeBuilder<Bucket> entityBuilder)
        {
            entityBuilder.HasKey(p => p.Id);

            entityBuilder.Property(p => p.Title)
                .IsRequired();
            
            entityBuilder.Property(p => p.UserId)
                .IsRequired();
            
            entityBuilder.HasOne(p => p.Currency)
                .WithMany()
                .HasForeignKey(p => p.CurrencyId)
                .IsRequired();
        }

        private static void BuildCurrencyType(EntityTypeBuilder<Currency> entityBuilder)
        {
            entityBuilder.HasKey(p => p.Id);
        }
        
        private static void BuildWalletType(EntityTypeBuilder<Wallet> entityBuilder)
        {
            entityBuilder.HasKey(wallet => wallet.Id);

            entityBuilder.Property(p => p.UserId)
                .IsRequired();

            entityBuilder.HasOne(p => p.Currency)
                .WithMany()
                .HasForeignKey(p => p.CurrencyId)
                .IsRequired();
        }
    }
}