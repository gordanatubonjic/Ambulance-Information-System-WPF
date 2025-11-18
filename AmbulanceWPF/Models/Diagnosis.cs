using AmbulanceWPF.Views;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("Diagnosis")]
    public class Diagnosis
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(13)]
        [ForeignKey("Patient")]
        public string PatientJMB { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Disease")]
        public int DiseaseCode { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Date { get; set; }

        [Column(TypeName = "TEXT")]
        public string DoctorOpinion { get; set; }

        [Required]
        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }

        [Required]
        [MaxLength(13)]
        [ForeignKey("Employee")]
        public string DoctorJMB { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual DiseaseCatalog Disease { get; set; }
        public virtual Examination Examination { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("PatientJMB")]
        public virtual MedicalRecord MedicalRecord { get; set; }
    }
}