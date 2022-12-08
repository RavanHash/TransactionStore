using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TransactionStore.Models;

namespace TransactionStore.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transaction>()
                .HasKey(c => c.TransactiontId);
            modelBuilder.Entity<Transaction>().HasData(
                new { UserId = 1, AccountId = 1, ReceiverId = 1, TransactiontId = 1});
            modelBuilder.Entity<Transaction>().HasData(
                new { UserId = 2, AccountId = 2, ReceiverId = 2, TransactiontId = 2});
        }
    }
}
