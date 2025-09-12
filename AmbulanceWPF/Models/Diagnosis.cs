using AmbulanceWPF.Views;

namespace AmbulanceWPF.Models
{
    public class Diagnosis
    {
        public  Patient? Patient { get; set; }

        public  Doctor? Doctor { get; set; }

        public int ICD_ID { get; set; } //sifra bolesti 

        public DateOnly Date { get; set; }

        public string? Opinion {  get; set; }


        public Diagnosis() { }

        public Diagnosis(Patient P) {
            Patient = P;
        
        }


    }
}