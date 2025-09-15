using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Models
{
    public class Employee
    {
        public string JMBG { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int? IsActive { get; set; }



        public Employee() { }

        public Employee(String username, String pass) {
            Username = username;
            Password = pass;
        }

        public override bool Equals(object obj)
        {

            return obj is Employee e && JMBG == e.JMBG;

        }
        public override int GetHashCode()
        {
            return -1221475543 + JMBG.GetHashCode();
        }

        public override string ToString()
        {
            return Name + ", " + Surname;
        }
    }
}
