using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("Therapy")]
    public class Therapy
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Intervention")]
        public int InterventionId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Medication")]
        public int MedicationCode { get; set; }

        [Column(TypeName = "REAL")]
        public decimal? Dosage { get; set; }

        public virtual Intervention Intervention { get; set; }
        public virtual MedicationCatalog Medication { get; set; }
    }
}