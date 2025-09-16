using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    public class Delivery
    {
        public int MedicationId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal Amount { get; set; }
        public Medication Medication { get; set; } = new Medication();
    }
}
