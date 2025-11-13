using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    [Table("Phone")]

    public class Phone
    {
        [Key]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Employee> Employees { get; set; }
    }
}
