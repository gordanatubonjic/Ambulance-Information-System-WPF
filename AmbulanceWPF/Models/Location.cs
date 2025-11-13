using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    [Table("Location")]
    public class Location
    {
        [Key]
        [Column("PostalCode")]
        public int PostalCode { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }
    }
}
