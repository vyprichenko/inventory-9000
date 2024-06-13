using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApplication.Models
{
    [Table("supplier")]
    public class Supplier
    {
        [Column("s_id")]
        public int Id { get; set; }

        [Required]
        [Column("s_name", TypeName = "nvarchar(255)")]
        public string Name { get; set; }
    }
}
