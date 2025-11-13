using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("Procurement")]
    public class Procurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProcurementId { get; set; }

        [Column(TypeName = "REAL")]
        public decimal? Quantity { get; set; }

        [Required]
        public DateTime ProcurementDate { get; set; }

                 public virtual ICollection<MedicationItem> MedicationItems { get; set; }
    }
}