using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("Intervention")]
    public class Intervention
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterventionId { get; set; }

        [Required]
        [MaxLength(13)]
        [ForeignKey("Patient")]
        public string PatientJMB { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string InterventionDescription { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual ICollection<InterventionDoctor> InterventionDoctors { get; set; }

        public virtual ICollection<Therapy> Therapies { get; set; }


    }
}