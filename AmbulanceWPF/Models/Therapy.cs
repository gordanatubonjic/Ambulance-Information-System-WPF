using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    public class Therapy
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public decimal Dosage { get; set; }
        public Intervention Intervention { get; set; } = new Intervention();
        public Medication Medication { get; set; } = new Medication();
    }
}
