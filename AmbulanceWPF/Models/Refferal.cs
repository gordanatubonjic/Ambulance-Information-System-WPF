namespace AmbulanceWPF.Models
{
    public class Refferal
    {
        public int Id { get; set; }
        public string JMBPatient { get; set; } = string.Empty;
        public int DiseaseId { get; set; }
        public string SpecialDoctor { get; set; } = string.Empty;
        public string JMBDoctor { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Patient Patient { get; set; } = new Patient();
        public Disease Disease { get; set; } = new Disease();
        public Doctor Doctor { get; set; } = new Doctor();
    }
}