using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApplication.Models
{
    [Owned]
    [Table("inventory_transfer_list")]
    public class InventoryTransferList
    {
        [Column("itl_transfer")]
        [ForeignKey("InventoryTransfer")]
        public int InventoryTransferId { get; set; }

        public InventoryTransfer InventoryTransfer { get; set; }

        [Column("itl_inventory")]
        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
