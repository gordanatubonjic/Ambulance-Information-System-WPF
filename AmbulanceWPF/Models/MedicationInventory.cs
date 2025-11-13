using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("MedicationInventory")]
    public class MedicationInventory
    {
        [Key]
        [ForeignKey("Medication")]
        public int MedicationCode { get; set; }

        [Required]
        [Column(TypeName = "REAL")]
        public decimal Quantity { get; set; }

                 public virtual MedicationCatalog Medication { get; set; }
    }
}
