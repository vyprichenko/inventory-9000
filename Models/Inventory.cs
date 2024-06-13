using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApplication.Models
{
    [Table("inventory")]
    public class Inventory
    {
        [Column("i_id")]
        public int Id { get; set; }

        [Column("i_sn", TypeName = "nchar(127)")]
        public string? SerialNumber { get; set; }

        [Required]
        [Column("i_name", TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(SerialNumber))
                {
                    return Name;
                }
                return string.Format("{0} ({1})", Name, SerialNumber.Trim());
            }
        }
    }
}
