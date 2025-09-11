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
            cmd.CommandText = "SELECT JMB, Ime, Prezime, Username, Password, Role, IsActive FROM `zaposleni` ORDER BY Username";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Employee()
                {
                    JMBG = reader.GetString(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Username =reader.GetString(3),
                    Password=reader.GetString(4),
                    Role=reader.GetString(5),
                    IsActive=reader.GetInt32(6)

                });

            }
            reader.Close();
            conn.Close();
            return result;
        }
        public static Employee GetEmployee(String username) {
            Employee? result = GetEmployees().Find(e=> e.Username == username);
            if (result == null)
                throw new Exception("User with such username doesnot exist!");

            return result;
        }
    
    }
}
