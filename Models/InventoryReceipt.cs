using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApplication.Models
{
    [Table("inventory_receipt")]
    public class InventoryReceipt
    {
        [Column("ir_id")]
        public int Id { get; set; }
        
        [Column("ir_price", TypeName = "decimal(10,0)")]
        public decimal Price { get; set; }
        
        [Column("ir_date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        
        [Column("ir_supplier")]
        [ForeignKey("Supplier")]
        public int SupplierId {  get; set; }
        
        public Supplier? Supplier {  get; set; }
        
        [Column("ir_inventory")]
        [ForeignKey("Inventory")]
        public int InventoryId {  get; set; }
        
        public Inventory? Inventory {  get; set; }
    }
}
