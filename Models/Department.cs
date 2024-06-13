using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApplication.Models
{
    [Table("department")]
    public class Department
    {
        [Column("d_id")]
        public int Id { get; set; }

        [Required]
        [Column("d_name", TypeName = "nvarchar(127)")]
        public string Name { get; set; }

        [Column("d_responsible")]
        [ForeignKey("ResponsibleEmployee")]
        public int? ResponsibleEmployeeId { get; set; }
        
        public Employee? ResponsibleEmployee { get; set; }
    }
}
