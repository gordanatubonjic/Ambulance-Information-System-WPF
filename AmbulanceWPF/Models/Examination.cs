using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("Examination")]
    public class Examination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExaminationId { get; set; }

        [Required]
        public DateTime ExaminationDate { get; set; }

        [MaxLength(45)]
        public string ExaminationDescription { get; set; }

        [Required]
        [MaxLength(13)]
        [ForeignKey("Patient")]
        public string PatientJMB { get; set; }

        [Required]
        [MaxLength(13)]
        [ForeignKey("Employee")]
        public string DoctorJMB { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ICollection<Diagnosis> Diagnoses { get; set; }

        public virtual ICollection<Referral> Referrals { get; set; }
    }
}