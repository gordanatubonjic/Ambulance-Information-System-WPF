using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    public class Medication
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }

        public Medication() { }

        public override bool Equals(object obj)
        {

            return obj is Medication e && Id == e.Id;

        }
        public override int GetHashCode()
        {
            return -1221475543 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id + ", " + Name;
        }
    }
}
