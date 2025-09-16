namespace AmbulanceWPF.Models
{
    public class Doctor : Employee
    {
        public string JMB { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Employee Employee { get; set; } = new Employee();
        public Phone Phone { get; set; } = new Phone();

    }
}