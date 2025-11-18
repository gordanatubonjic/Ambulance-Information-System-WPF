using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("MedicalRecord")]
    public class MedicalRecord
    {
        [Key]
        [Required]
        [MaxLength(13)]
        [ForeignKey("PatientJMB")]
        public string PatientJMB { get; set; }

        [Required]
        [MaxLength(45)]
        public string ParentName { get; set; }

        [Required]
        [MaxLength(10)]
        public string MaritalStatus { get; set; }

        [Required]
        public bool Gender { get; set; }
        [Required]
        public bool Insurance { get; set; }

        [Required]
        [MaxLength(13)]
        [ForeignKey("Employee")]
        public string DoctorJMB { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();
        public virtual ICollection<Referral> Referrals { get; set; } = new List<Referral>();
        public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();


        public MedicalRecord() { }

        public override bool Equals(object obj)
        {

            return obj is MedicalRecord e && Patient == e.Patient;

        }
        public override int GetHashCode()
        {
            return -1221475543 + Patient.GetHashCode();
        }

        public override string ToString()
        {
            //TODO
            return "";
        }
    }
}

