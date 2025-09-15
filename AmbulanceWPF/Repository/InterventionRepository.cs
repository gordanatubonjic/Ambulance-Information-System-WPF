using AmbulanceWPF.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Repository
{
    public static  class InterventionRepository
    {
        private static readonly string connectionString = "Server=localhost;Port=3306;Database=mydb;UserId=root;Password=student";

        public static List<Intervention> GetInterventions(String JMBDoctor)
        {
            List<Intervention> result = new List<Intervention>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM mydb.intervencija AS I INNER JOIN mydb.intervencija_has_ljekar AS IL ON I.IdIntervencije=IL.IdIntervencije WHERE IL.JMBLjekara=@JMBDoctor";
            cmd.Parameters.AddWithValue("@JMBDoctor", JMBDoctor);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Intervention()
                {
                    Id = reader.GetInt32(0),
                    Patient = reader.GetString(1),
                    Date = DateOnly.FromDateTime(reader.GetDateTime(2)),
                    Description=reader.GetString(3),
                    Doctor=reader.GetString(5)
                });

            }
            reader.Close();
            conn.Close();
            return result;
        }
        public static bool AddIntervention(Intervention intervention)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO intervencija (JMBPacijenta, Datum, OpisIntervencije) 
                               VALUES (@Patient, @Date, @Description, @Doctor)";

                    cmd.Parameters.AddWithValue("@Patient", intervention.Patient);
                    cmd.Parameters.AddWithValue("@Date", intervention.Date);
                    cmd.Parameters.AddWithValue("@Description", intervention.Description);
                   // cmd.Parameters.AddWithValue("@Doctor", intervention.Doctor);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding intervention: {ex.Message}");
                return false;
            }
        }

    }
}
