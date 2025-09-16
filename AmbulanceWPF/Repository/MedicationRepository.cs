using AmbulanceWPF.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Repository
{
    public static class MedicationRepository
    {

        private static readonly string connectionString = "Server=localhost;Port=3306;Database=mydb;UserId=root;Password=student";

        public static List<Medication> GetMedication()
        {
            List<Medication> result = new List<Medication>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT SifraLijeka, Naziv, Proizvodjac FROM `sifarnik lijekova` ORDER BY Name";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Medication()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Manufacturer = reader.GetString(2)                   

                });

            }
            reader.Close();
            conn.Close();
            return result;
        }
        

    }
}
