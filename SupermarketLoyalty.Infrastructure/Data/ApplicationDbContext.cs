using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupermarketLoyalty.Core.Entities;

namespace SupermarketLoyalty.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<RewardRedemption> RewardRedemptions { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignProduct> CampaignProducts { get; set; }
        public DbSet<ExpiringPoint> ExpiringPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customer configuration
            builder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.MembershipNumber).IsUnique();
                entity.Property(e => e.Tier).HasConversion<int>();
            });

            // AdminUser configuration
            builder.Entity<AdminUser>(entity =>
            {
                entity.Property(e => e.Role).HasConversion<int>();
            });

            // Product configuration
            builder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.PointsPerDollar).HasPrecision(18, 2);
            });

            // Transaction configuration
            builder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Subtotal).HasPrecision(18, 2);
                entity.Property(e => e.Type).HasConversion<int>();
                entity.HasIndex(e => e.TransactionNumber).IsUnique();
                
                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.Transactions)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // TransactionItem configuration
            builder.Entity<TransactionItem>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
                
                entity.HasOne(e => e.Transaction)
                    .WithMany(e => e.Items)
                    .HasForeignKey(e => e.TransactionId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(e => e.Product)
                    .WithMany(e => e.TransactionItems)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Reward configuration
            builder.Entity<Reward>(entity =>
            {
                entity.Property(e => e.RequiredTier).HasConversion<int?>();
            });

            // RewardRedemption configuration
            builder.Entity<RewardRedemption>(entity =>
            {
                entity.Property(e => e.Status).HasConversion<int>();
                
                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.RewardRedemptions)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(e => e.Reward)
                    .WithMany(e => e.RewardRedemptions)
                    .HasForeignKey(e => e.RewardId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Campaign configuration
            builder.Entity<Campaign>(entity =>
            {
                entity.Property(e => e.BonusMultiplier).HasPrecision(18, 2);
            });

            // CampaignProduct configuration
            builder.Entity<CampaignProduct>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.ProductId });
                
                entity.HasOne(e => e.Campaign)
                    .WithMany(e => e.CampaignProducts)
                    .HasForeignKey(e => e.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(e => e.Product)
                    .WithMany(e => e.CampaignProducts)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ExpiringPoint configuration
            builder.Entity<ExpiringPoint>(entity =>
            {
                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.ExpiringPoints)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}