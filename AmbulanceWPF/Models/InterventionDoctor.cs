using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("InterventionDoctor")]
    public class InterventionDoctor
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Intervention")]
        public int InterventionId { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(13)]
        [ForeignKey("Employee")]
        public string DoctorJMB { get; set; }

                 public virtual Intervention Intervention { get; set; }
        public virtual Employee Employee { get; set; }
    }
}