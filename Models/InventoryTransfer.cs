using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApplication.Models
{
    [Table("inventory_transfer")]
    public class InventoryTransfer
    {
        [Column("it_id")]
        public int Id { get; set; }
        
        [Column("it_date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        
        [Column("it_operation")]
        [ForeignKey("Operation")]
        public int OperationId {  get; set; }
       
        public Operation? Operation {  get; set; }
        
        [Column("it_signatory")]
        [ForeignKey("Signatory")]
        public int SignatoryId { get; set; }
        
        public Employee? Signatory { get; set; }
        
        [Column("it_department")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        
        public Department? Department { get; set; }
        
        [Column("it_department_responsible")]
        [ForeignKey("DepartmentResponsibleEmployee")]
        public int? DepartmentResponsibleEmployeeId { get; set; }
        
        public Employee? DepartmentResponsibleEmployee { get; set; }

        public List<InventoryTransferList> Inventories { get; set; } = new List<InventoryTransferList>();
    }
}
