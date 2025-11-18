using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("Referral")]
    public class Referral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReferralId { get; set; }

        [Required]
        [ForeignKey("Disease")]
        public int DiseaseCode { get; set; }

        [Required]
        [MaxLength(45)]
        public string Specialists { get; set; }

        [Required]
        [MaxLength(13)]
        [ForeignKey("Employee")]
        public string DoctorJMB { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        public string PatientJMB { get; set; }

        public virtual DiseaseCatalog Disease { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Examination Examination { get; set; }

        [ForeignKey("PatientJMB")]
        public virtual MedicalRecord MedicalRecord { get; set; }
    }
}