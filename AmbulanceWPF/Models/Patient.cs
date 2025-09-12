using AmbulanceWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    public class Patient
    {

        public string JMB { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public PatientHistory History { get; set; }
        public Patient() { }

        public override bool Equals(object obj)
        {

            return obj is Patient e && JMB == e.JMB;

        }
        public override int GetHashCode()
        {
            return -1221475543 + JMB.GetHashCode();
        }

        public override string ToString()
        {
            return Name + ", " + Surname;
        }
    }
}
