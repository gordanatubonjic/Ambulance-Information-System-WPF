using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    public class PatientHistory
    {
        public Patient Patient { get; set; }

        public string? ParentsName { get; set; }

        public string? MaritalStatus { get; set; }
        public Boolean? Sex { get; set; }

        public Boolean? Insurance { get; set; }

        public Employee FamilyDoctor { get; set; } //TODO Promijeniti na Doctor model kad napravim lol

        public List<Diagnosis> Diagnosis { get; set;  }
        public List<Refferal> Refferals { get; set;  }



        public PatientHistory() { }



        public override bool Equals(object obj)
        {

            return obj is PatientHistory e && Patient == e.Patient;

        }
        public override int GetHashCode()
        {
            return -1221475543 + Patient.GetHashCode();
        }

        public override string ToString()
        {
            //TODO
            return "";
        }
    }
}

