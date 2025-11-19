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
using System.Text.RegularExpressions;
using System.Xml.Linq;

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

        public String Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage)); // For visibility binding
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set { _isPasswordVisible = value; OnPropertyChanged(nameof(IsPasswordVisible)); }
        }
        //QA Za sta sam ovo koristila
        static public BooleanToVisibilityConverter BooleanToVisibilityConverter = new BooleanToVisibilityConverter();
        static public InverseBooleanToVisibilityConverter InverseBooleanToVisibilityConverter = new InverseBooleanToVisibilityConverter();
        static public PasswordVisibilityToIconConverter PasswordVisibilityToIconConverter = new PasswordVisibilityToIconConverter();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand LoginCommand { get; set; }
        public ICommand TogglePasswordCommand { get; }

        // In constructor:

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
            Password = "lozinka";
            TogglePasswordCommand = new RelayCommand(() => IsPasswordVisible = !IsPasswordVisible);


        }

        private async void Login()
        {
            ErrorMessage = string.Empty;
            if (Username == null || Password ==null)
            {
                ErrorMessage = "Username or Password must be put in.";
                return;
            }

            Employee? employee = null;
            bool loginSuccess = false;

            try
            {
                // 2. Find user by username (case-sensitive or use .ToLower() if needed)
                employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Username == Username);

                if (employee == null)
                {
                    ErrorMessage = "Username or Password are wrong. Try again!";
                    return;
                }

                // 3. Check if account is active
                if (employee.IsActive != 1)
                {
                    ErrorMessage = "User is no longer active in system!";
                    return;
                }

               
                loginSuccess = Password == employee.Password;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Something went wrong with your login!";

                return;
            }
            finally
            {
            
                Password = string.Empty;
            }

            // 5. Final login result
            if (!loginSuccess)
            {
                ErrorMessage = "Username or Password are wrong. Try again!";
                return;
            }

            ApplyUserPreferences(employee.Theme, employee.Language);

            // 6. Open correct window based on role
            Window nextWindow = employee.Role switch
            {
                "Doctor" => new DoctorHomePageView(employee),
                "MedicalTechnician" => new TechnicianHomePageView(employee),
                _ => new LoginFormView() // fallback
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

        private void ApplyUserPreferences(string themeName, string langCode)
        {
            string themePath = themeName switch
            {
                "Light" => "Themes/Light.xaml",
                "Blue" => "Themes/Blue.xaml",
                "Dark" => "Themes/Dark.xaml",
                _ => "Themes/Light.xaml"
            };

            string langPath = langCode == "Serbian"
                ? "LanguageDictionaries/SerbianDictionary.xaml"
                : "LanguageDictionaries/EnglishDictionary.xaml";

            var themeDict = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };
            var langDict = new ResourceDictionary { Source = new Uri(langPath, UriKind.Relative) };
            var materialDesignDefaults = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml")
            };

            for (int i = Application.Current.Resources.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var md = Application.Current.Resources.MergedDictionaries[i];
                if (md.Source != null &&
                    (md.Source.OriginalString.Contains("Theme") ||
                     md.Source.OriginalString.Contains("MaterialDesign3.Defaults.xaml") ||
                     md.Source.OriginalString.Contains("Languages/")))
                {
                    Application.Current.Resources.MergedDictionaries.RemoveAt(i);
                }
            }

            Application.Current.Resources.MergedDictionaries.Add(materialDesignDefaults);
            Application.Current.Resources.MergedDictionaries.Add(themeDict);
            Application.Current.Resources.MergedDictionaries.Add(langDict);
        }




    }
}
