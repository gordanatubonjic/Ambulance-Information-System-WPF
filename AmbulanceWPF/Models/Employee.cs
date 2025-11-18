using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [MaxLength(13)]
        public string JMB { get; set; }
        [Required]
        [MaxLength(45)]
        public string Name { get; set; }
        [Required]
        [MaxLength(45)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(45)]
        public string Username { get; set; }
        [Required]
        [MaxLength(45)]
        public string Password { get; set; }
        [Required]
        [MaxLength(64)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(45)]
        public string Role { get; set; }
        public int IsActive { get; set; }

        [Required]
        [MaxLength(100)]
        public string Theme { get; set; }
                 [MaxLength(20)]
        public string? PhoneNumber { get; set; }

                 [ForeignKey("PhoneNumber")]
        public virtual Phone? Phone { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public virtual ICollection<Examination> Examinations { get; set; }
        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
        public virtual ICollection<InterventionDoctor> InterventionDoctors { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }

                public string FullName => $"{Name} {LastName}";

        public Employee() { }
        public Employee(String username, String pass) {
            Username = username;
            Password = pass;
        }

        public override bool Equals(object obj)
        {

            return obj is Employee e && JMB == e.JMB;

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
