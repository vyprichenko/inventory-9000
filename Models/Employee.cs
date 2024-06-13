using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApplication.Models
{
    [Table("employee")]
    public class Employee
    {
        [Column("e_id")]
        public int Id { get; set; }

        [Required]
        [Column("e_first_name", TypeName = "nvarchar(127)")]
        public string FirstName { get; set; }

        [Column("e_last_name", TypeName = "nvarchar(127)")]
        public string? LastName { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName).Trim();
            }
        }
    }
}
