using AmbulanceWPF.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Repository
{
    static class EmployeeRepository
    {
        private static readonly string connectionString = "Server=localhost;Port=3306;Database=mydb;UserId=root;Password=student";

        public static List<Employee> GetEmployees()
        {
            List<Employee> result = new List<Employee>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT JMB, Ime, Prezime FROM `zaposleni`   ORDER BY Ime";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Employee()
                {
                    JMBG = reader.GetString(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2)

                });

            }
            reader.Close();
            conn.Close();
            return result;
        }
    }
}
