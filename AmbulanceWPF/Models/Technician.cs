using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    public class Technician
    {
        public string JMB { get; set; } = string.Empty;
        public Employee Employee { get; set; } = new Employee();
    }
}
