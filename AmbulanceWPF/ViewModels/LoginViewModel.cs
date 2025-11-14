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
using AmbulanceWPF.Helper;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceWPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
       static List<Employee> employees;
        private readonly AmbulanceDbContext _context;
        private string _username = string.Empty;
        //private SecureString _password = new SecureString();
        private String _password = string.Empty;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username));
            }
        }

        /* public SecureString Password
         {
             get => _password;
             set { _password = value ?? new SecureString(); OnPropertyChanged(nameof(Password)); }
         }*/
        public String Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        //QA Za sta sam ovo koristila
        static public BooleanToVisibilityConverter BooleanToVisibilityConverter = new BooleanToVisibilityConverter();
        static public InverseBooleanToVisibilityConverter InverseBooleanToVisibilityConverter = new InverseBooleanToVisibilityConverter();
        static public PasswordVisibilityToIconConverter PasswordVisibilityToIconConverter = new PasswordVisibilityToIconConverter();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand LoginCommand { get; set; }
           

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
            Username = "amar.k";
            Password = "amarovasifra";

        }

        private async void Login()
        {
            // 1. Validate input
            /*if (string.IsNullOrWhiteSpace(Username))
            {
                Console.WriteLine("Unesite korisničko ime.");
                return;
            }

            if (Password == null || Password.Length == 0)
            {
                Console.WriteLine("Unesite lozinku.");
                return;
            }*/

            Employee? employee = null;
            bool loginSuccess = false;

            try
            {
                // 2. Find user by username (case-sensitive or use .ToLower() if needed)
                employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Username == Username);

                if (employee == null)
                {
                    Console.WriteLine("Nespješan login: Korisnik ne postoji.");
                    return;
                }

                // 3. Check if account is active
                if (employee.IsActive != 1)
                {
                    Console.WriteLine("Korisnik nema pravo logina!");
                    return;
                }

                // 4. Verify password hash
               /* if (string.IsNullOrEmpty(employee.PasswordHash))
                {
                    Console.WriteLine("Greška: Lozinka nije postavljena.");
                    return;
                }*/

                //loginSuccess = PasswordHasher.Verify(Password, employee.PasswordHash);
                loginSuccess = Password == employee.Password;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri prijavi: {ex.Message}");
                return;
            }
            finally
            {
                // Always clear password from memory
                //Password?.Clear();
                Password = string.Empty;
            }

            // 5. Final login result
            if (!loginSuccess)
            {
                Console.WriteLine("Nespješan login: Pogrešna lozinka.");
                return;
            }

            Console.WriteLine("Uspješan login!");

            // 6. Open correct window based on role
            Window nextWindow = employee.Role switch
            {
                "Doctor" => new DoctorHomePageView(employee),
                "MedicalTechnician" => new TechnicianHomePageView(employee),
                _ => new DoctorHomePageView(employee) // fallback
            };

            nextWindow.Show();

            // 7. Close login window
            var loginWindow = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.Title == "LoginFormView");

            loginWindow?.Close();
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
