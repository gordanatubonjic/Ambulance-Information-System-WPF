using AmbulanceWPF.Models;
using AmbulanceWPF.Repository;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AmbulanceWPF.ViewModels
{
    public class LoginViewModel
    {
        List<Employee> employees;
        public ICommand LoginCommand { get; set; }
        public string? Username
        {
            get;
            set;
        }

        public string? Password
        {
            get;
            set;
        }



        public LoginViewModel()
        {

            LoginCommand = new RelayCommand(Login, CanLogin);
            employees = EmployeeRepository.GetEmployees();
        }

        private void Login()
        {
            foreach (var employee in employees)
                if (employee.Username == Username && employee.Password == Password)
                {
                    Console.WriteLine("Uspjesan login!!");
                    break;
                }
                else
                {
                    Console.WriteLine("Nespjesan login!!");

                };
        }
        private Boolean CanLogin()
        {
            return true;
        }


    }
}
