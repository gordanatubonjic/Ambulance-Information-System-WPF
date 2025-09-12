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
            cmd.CommandText = "SELECT * FROM mydb.intervencija AS I INNER JOIN mydb.intervencija_has_ljekar AS IL ON I.IdIntervencije=IL.IdIntervencije WHERE IL.JMBLjekara=JMBDoctor";
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

    }
}
