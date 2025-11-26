using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace InventorySystem.API.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<ItemTransaction> ItemTransactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemId);
                entity.Property(e => e.ItemId)
                      .ValueGeneratedOnAdd(); 
                entity.Property(e => e.ItemName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ItemBrand).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ItemDescription).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ItemPrice).IsRequired();
                entity.Property(e => e.QuantityAvailable).IsRequired();
                entity.Property(e => e.CriticalLevel).IsRequired();
            });

            modelBuilder.Entity<ItemTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);
                entity.Property(e => e.ItemId).IsRequired();
                entity.Property(e => e.Total).IsRequired();
                entity.Property(e => e.TransactionDate).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.IsCompleted).IsRequired();
                entity.HasOne(e => e.Item)
                      .WithMany()
                      .HasForeignKey(e => e.ItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
