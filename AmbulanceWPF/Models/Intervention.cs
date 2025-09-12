using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    public class Intervention
    {

        public int Id { get; set; }

        public string? Patient {  get; set; }
        public DateOnly Date { get; set; }
        public string? Description {  get; set; }

        public string? Doctor { get; set; }
    
    
    }
}
