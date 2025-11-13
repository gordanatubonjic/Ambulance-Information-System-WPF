using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("MedicationCatalog")]
    public class MedicationCatalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MedicationCode { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        [MaxLength(45)]
        public string Manufacturer { get; set; }

        public bool IsActive { get; set; } = true;

                 public virtual MedicationInventory MedicationInventory { get; set; }
        public virtual ICollection<Therapy> Therapies { get; set; }
        public virtual ICollection<MedicationItem> MedicationItems { get; set; }
 
public MedicationCatalog() { }

        public override bool Equals(object obj)
        {

            return obj is MedicationCatalog e && MedicationCode == e.MedicationCode;

        }
        public override int GetHashCode()
        {
            return -1221475543 + MedicationCode.GetHashCode();
        }

        public override string ToString()
        {
            return MedicationCode + ", " + Name;
        }
    }
}
