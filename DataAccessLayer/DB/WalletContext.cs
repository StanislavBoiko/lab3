using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DB
{
    public class WalletContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=WalletDB;Trusted_Connection=True;TrustServerCertificate=True");
        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Transaction>().HasOne<Account>(t => t.Sender).WithMany(a => a.Outgoing).HasForeignKey(t => t.SenderId);
            modelBuilder.Entity<Transaction>().HasOne<Account>(t => t.Recipient).WithMany(a => a.Incoming).HasForeignKey(t => t.RecipientId);
            modelBuilder.Entity<Account>().Property(a => a.Name).IsRequired();

        }
    }
}
