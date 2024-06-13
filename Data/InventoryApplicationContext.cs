using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApplication.Models;
using System.Runtime.ConstrainedExecution;
using System.Reflection.Metadata;

namespace InventoryApplication.Data
{
    public class InventoryApplicationContext : DbContext
    {
        public InventoryApplicationContext(DbContextOptions<InventoryApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryTransfer>().OwnsMany(it => it.Inventories, itl =>
            {
                itl.WithOwner(c => c.InventoryTransfer);
                itl.HasKey(c => new { c.InventoryTransferId, c.InventoryId });
            });

            /*
             * modelBuilder.Entity<InventoryTransferList>()
                .HasKey(it => new { it.InventoryTransferId, it.InventoryId });

            modelBuilder.Entity<InventoryTransferList>()
                .HasOne(it => it.InventoryTransfer)
                .WithMany(it => it.Inventories)
                .HasForeignKey(it => it.InventoryTransferId);

            modelBuilder.Entity<InventoryTransferList>()
                .HasOne(it => it.Inventory)
                .WithMany()
                .HasForeignKey(it => it.InventoryId);
            */
        }

        public DbSet<InventoryApplication.Models.Supplier> Supplier { get; set; } = default!;
        public DbSet<InventoryApplication.Models.Employee> Employee { get; set; } = default!;
        public DbSet<InventoryApplication.Models.Department> Department { get; set; } = default!;
        public DbSet<InventoryApplication.Models.Operation> Operation { get; set; } = default!;
        public DbSet<InventoryApplication.Models.InventoryReceipt> InventoryReceipt { get; set; } = default!;
        public DbSet<InventoryApplication.Models.Inventory> Inventory { get; set; } = default!;
        public DbSet<InventoryApplication.Models.InventoryTransfer> InventoryTransfer { get; set; } = default!;
    }
}
