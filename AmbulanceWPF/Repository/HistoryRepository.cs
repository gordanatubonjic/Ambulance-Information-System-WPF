using AmbulanceWPF.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceWPF.Repository
{
    class HistoryRepository
    {
        private static readonly string connectionString = "Server=localhost;Port=3306;Database=mydb;UserId=root;Password=student";

        public static PatientHistory GetPatientHistory(String JMB)
        {
            List<Diagnosis> diagnosis = new List<Diagnosis>();
            List<Refferal> refferals = new List<Refferal>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT JMBPacijenta, SifraBolesti, Datum, MisljenjneLjekara, JMBLjekara FROM `dijagnoza` WHERE JMBGPacijenta=JMB ORDER BY Datum";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                diagnosis.Add(new Diagnosis()
                {
                    ICD_ID = reader.GetInt32(1),
                    Date = DateOnly.FromDateTime(reader.GetDateTime(2)),
                    Opinion = reader.GetString(3),
                    Doctor = new Doctor() { JMBG = reader.GetString(4) }
                });

            }
            cmd.CommandText = "SELECT * FROM `uputnice` WHERE JMBPacijenta=JMB ORDER BY Datum";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                refferals.Add(new Refferal()
                {
                    //TODO



                });
            }
            cmd.CommandText = "SELECT * FROM `karton` WHERE JMBPacijenta=JMB";
            reader = cmd.ExecuteReader();
            PatientHistory history = new PatientHistory();
            history.Diagnosis = diagnosis;
            history.Refferals = refferals;
            while (reader.Read())
            {
                history.ParentsName
                    = reader.GetString(1);
                history.MaritalStatus = reader.GetString(2);
                history.Sex=reader.GetBoolean(3);
                history.Insurance=reader.GetBoolean(4);
                history.FamilyDoctor = EmployeeRepository.GetEmployee(reader.GetString(5));

            }
            reader.Close();
            conn.Close();
            return history;
        }
        

    }
}
