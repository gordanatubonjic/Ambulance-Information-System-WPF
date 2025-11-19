using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceWPF.Models
{
    [Table("DiseaseCatalog")]
    public class DiseaseCatalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DiseaseCode { get; set; }

        [Required]
        [MaxLength(45)]
        public string DiseaseName { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string Description { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
    }
}
