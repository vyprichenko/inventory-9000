using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApplication.Models
{
    [Table("operation")]
    public class Operation
    {
        [Column("o_id")]
        public int Id { get; set; }

        [Required]
        [Column("o_name", TypeName = "nvarchar(45)")]
        public string Name { get; set; }
    }
}
