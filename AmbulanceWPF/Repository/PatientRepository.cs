using AmbulanceWPF.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Repository
{
    class PatientRepository
    {

        private static readonly string connectionString = "Server=localhost;Port=3306;Database=mydb;UserId=root;Password=student";

        public static List<Patient> GetPatients()
        {
            List<Patient> result = new List<Patient>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT JMBPacijenta, Ime, Prezime, MjestoPrebivalista FROM `pacijent` ORDER BY Ime";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Patient()
                {
                    JMB = reader.GetString(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                   City = reader.GetInt32(3),
                   History=HistoryRepository.GetPatientHistory(reader.GetString(0))
                });

            }
            reader.Close();
            conn.Close();
            return result;
        }
        public static Patient GetPatient(String JMB)
        {
            Patient? result = GetPatients().Find(e => e.JMB == JMB);
            if (result == null)
                throw new Exception("User with such username doesnot exist!");

            return result;
        }

    }
}
