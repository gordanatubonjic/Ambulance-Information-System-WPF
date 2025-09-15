using AmbulanceWPF.Models;
using AmbulanceWPF.Repository;
using AmbulanceWPF.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceWPF.ViewModels
{
    public class LoginViewModel
    {
       static List<Employee> employees;

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
      
        public int? Active
        {
            get;
            set;
        }


        public LoginViewModel()
        {
            //DEMO
            Username = "markic";
            Password = "markic";
            //END_DEMO
            LoginCommand = new RelayCommand(Login, CanLogin);
            employees = EmployeeRepository.GetEmployees();
        }

        private void Login()
        {
            foreach (var employee in employees)
                if (employee.Username == Username && employee.Password == Password)
                {
                    if (employee.IsActive == 1)
                    {
                        Console.WriteLine("Uspjesan login!!");
                        //TODO otvara prozor u zavisnosti od uloge?

                        Window nextWindow = employee.Role == "ljekar"
                         ? new DoctorHomePageView(employee)
                             : new TechnicianHomePageView(employee);

                        nextWindow.Show();
                        foreach (Window win in Application.Current.Windows)
                        {
                            if (win is Window && win.Title == "LoginFormView")
                            {
                                win.Close();
                                break;
                            }
                        }
                    }
                    else
                        Console.WriteLine("Korisnik nema pravo logina!");
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
