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
            List<Employee> lista =  GetEmployees();
            //DEBUG
            Console.WriteLine("Available usernames:");
            foreach (var emp in lista)
            {
                Console.WriteLine($"'{emp.Username}'");
            }
            Console.WriteLine($"Looking for: '{username}'");
            //END_OF_DEBUG
            // Employee? result = lista.Find(e=> (e.Username!= null ? e.Username.Trim() : "") == username.Trim());
            Employee? result = lista[0];
            if (result == null)
                throw new Exception("User with such username doesnot exist!");

            return result;
        }

        public static bool UpdateEmployee(Employee employee)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"UPDATE zaposleni 
                               SET Ime = @Name, Prezime = @Surname, 
                                   Username = @Username, Password = @Password 
                               WHERE JMB = @JMBG";

                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Surname", employee.Surname);
                    cmd.Parameters.AddWithValue("@Username", employee.Username);
                    cmd.Parameters.AddWithValue("@Password", employee.Password);
                    cmd.Parameters.AddWithValue("@JMBG", employee.JMBG);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
                return false;
            }
        }

    }

}
