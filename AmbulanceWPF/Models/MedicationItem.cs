using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("MedicationItem")]
    public class MedicationItem
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Medication")]
        public int MedicationCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Procurement")]
        public int ProcurementId { get; set; }

        [Required]
        public int Quantity { get; set; }

                 public virtual MedicationCatalog Medication { get; set; }
        public virtual Procurement Procurement { get; set; }
    }
}