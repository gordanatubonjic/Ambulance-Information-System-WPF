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
        public string JMBPatient {  get; set; }
        public DateOnly Date { get; set; }
        public string? Description {  get; set; }
        public Patient Patient {  get; set; }
        public List<Doctor> Doctor { get; set; }

        public List<Therapy> Therapies { get; set; } = new List<Therapy>();
    
    }
}
