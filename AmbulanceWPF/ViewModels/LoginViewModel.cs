using AmbulanceWPF.Models;
using AmbulanceWPF.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AmbulanceWPF.Converters;
using System.Security;
using AmbulanceWPF.Data;
using System.Security.Cryptography.Xml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;

namespace AmbulanceWPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
       static List<Employee> employees;
        private readonly AmbulanceDbContext _context;
        private string _password; 

        //QA Za sta sam ovo koristila
        static public BooleanToVisibilityConverter BooleanToVisibilityConverter = new BooleanToVisibilityConverter();
        static public InverseBooleanToVisibilityConverter InverseBooleanToVisibilityConverter = new InverseBooleanToVisibilityConverter();
        static public PasswordVisibilityToIconConverter PasswordVisibilityToIconConverter = new PasswordVisibilityToIconConverter();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand LoginCommand { get; set; }
        public string? Username
        {
            get;
            set;
        }

        //public SecureString? Password
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }



        public int? Active
        {
            get;
            set;
        }


        public LoginViewModel()
        {
            
            _context = new AmbulanceDbContext();
            LoginCommand = new RelayCommand(Login, CanLogin);
            //QA Da li mi treba ovo uopste
            employees = _context.Employees.ToList<Employee>();
        }

        private void Login()
        {
            Employee? employee = _context.Employees.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (employee!=null)
                {
                    if (employee.IsActive == 1)
                    {
                        Console.WriteLine("Uspjesan login!!");
                        //TODO otvara prozor u zavisnosti od uloge?

                        Window nextWindow = employee.Role == "Employee"
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
                
            }
                else
                {
                    Console.WriteLine("Nespjesan login!!");
                Window nextWindow = new DoctorHomePageView(employee);


                };
        }
        private Boolean CanLogin()
        {
             return true;
        }

       /* public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            
                command.CommandText = "select *from [User] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }*/


    }
}
