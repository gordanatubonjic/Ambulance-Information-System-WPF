using AmbulanceWPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Key]
        [Required]
        [MaxLength(13)]
        [Column("JMB")]
        public string JMB { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        [MaxLength(45)]
        public string LastName { get; set; }

        [Required]
        [ForeignKey("ResidenceLocation")]
        public int ResidenceLocationId { get; set; }
        //TODO dodati u bazu kolonu alergije, optional 0..*
        [Column(TypeName = "TEXT")]
        public string Allergies { get; set; } = string.Empty;
        public ICollection<Examination> Examinations { get; set; } = new List<Examination>();

        public virtual Location ResidenceLocation { get; set; }
        [Column("Insurance")]
        public Boolean Insurance {  get ; set; }
        [Column("DateOfBirth")]

        public string DateOfBirth {  get; set; }
        [Column("Gender")]
        public Boolean Gender { get; set; }


        public string FullName => $"{Name} {LastName}";

        public Patient() { }

        public override bool Equals(object obj)
        {

            return obj is Patient e && JMB == e.JMB;

        }
        public override int GetHashCode()
        {
            return -1221475543 + JMB.GetHashCode();
        }

        public override string ToString()
        {
            return Name + ", " + LastName;
        }
    }
}
