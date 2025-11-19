using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AmbulanceWPF.Data;
using AmbulanceWPF.Models;
using System.Text.RegularExpressions;


namespace AmbulanceWPF.ViewModels
{
        public class ProfileViewModel : INotifyPropertyChanged, IDataErrorInfo
        {
            private Employee _currentEmployee;
            private string _name;
            private string _surname;
            private string _username;
            private string _password;
            private string _passwordRepeat;
            private bool _isValid;

        private AmbulanceDbContext _context = new AmbulanceDbContext();
            public event PropertyChangedEventHandler PropertyChanged;

            public ICommand SaveCommand { get; }
            public ICommand CancelCommand { get; }

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                    Validate();
                }
            }

            public string LastName
            {
                get => _surname;
                set
                {
                    _surname = value;
                    OnPropertyChanged(nameof(LastName));
                    Validate();
                }
            }

            public string Username
            {
                get => _username;
                set
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                    Validate();
                }
            }

            public string Password
            {
                get => _password;
                set
                {
                    _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(PasswordRepeat)); // This triggers PasswordRepeat validation
                Validate();
            }
            }public string PasswordRepeat
            {
                get => _passwordRepeat;
                set
                {
                _passwordRepeat = value;
                    OnPropertyChanged(nameof(PasswordRepeat));
                    Validate();
                }
            }

            public bool IsValid
            {
                get => _isValid;
                set
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }

        
        public ProfileViewModel(Employee e)
            {
                             _currentEmployee = e;
                LoadCurrentUser(e);

                SaveCommand = new RelayCommand(SaveChanges, CanSaveChanges);
                CancelCommand = new RelayCommand(CancelChanges);
            }

            private void LoadCurrentUser(Employee _currentEmployee)
            {
                                                 
                if (_currentEmployee != null)
                {
                    Name = _currentEmployee.Name;
                    LastName = _currentEmployee.LastName;
                    Username = _currentEmployee.Username;
                    Password = _currentEmployee.Password;
                    PasswordRepeat = _currentEmployee.Password;
                }
            }

            private void SaveChanges()
            {
            if (!IsValid)
            {
                MessageBox.Show("Profile cannot be updated successfully!", "Retry", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            

            try
                {
                    _currentEmployee.Name = Name;
                    _currentEmployee.LastName = LastName;
                    _currentEmployee.Username = Username;
                    _currentEmployee.Password = Password;
                    
                using (var context = new AmbulanceDbContext())
                {
                    // Mark the entity as modified
                    context.Employees.Update(_currentEmployee);

                    // Save changes to database
                    context.SaveChanges();
                }

                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private bool CanSaveChanges()
            {
                return IsValid;
            }

            private void CancelChanges()
            {
               LoadCurrentUser(_currentEmployee); // Reload original values
            
            }

        private void Validate()
        {
            // Basic validation
            bool hasBasicValues = !string.IsNullOrEmpty(Name) &&
                                 !string.IsNullOrEmpty(LastName) &&
                                 !string.IsNullOrEmpty(Username) &&
                                 !string.IsNullOrEmpty(Password)&&
                                 !string.IsNullOrEmpty(PasswordRepeat);

            if (!hasBasicValues )
            {
                IsValid = false;
                return;
            }

            // Name and LastName: First uppercase, rest lowercase
            Regex nameRegex = new Regex(@"^[A-Z][a-z]+$");
            bool isValidName = nameRegex.IsMatch(Name);
            bool isValidLastName = nameRegex.IsMatch(LastName);

            // Username: only lowercase letters and/or dots
            Regex usernameRegex = new Regex(@"^[a-z.]+$");
            bool isValidUsername = Username.Length >= 6 && usernameRegex.IsMatch(Username) && !_context.Employees.Any(e=>e.Username == Username);

            // Password: lowercase, uppercase, numbers, and special characters !, #, $
            Regex passwordRegex = new Regex(@"([a-z]|[A-Z]|[0-9]|[!#$])+");
            bool isValidPassword = Password.Length >= 6 && passwordRegex.IsMatch(Password);

            if (Password != PasswordRepeat)
            {
                IsValid = false;
                return;
            }
            IsValid = isValidName && isValidLastName && isValidUsername && isValidPassword;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name))
                            return "First name is required";
                        if (!Regex.IsMatch(Name, @"^[A-Z][a-z]+$"))
                            return "First name must start with uppercase and rest lowercase letters";
                        break;

                    case nameof(LastName):
                        if (string.IsNullOrEmpty(LastName))
                            return "Last name is required";
                        if (!Regex.IsMatch(LastName, @"^[A-Z][a-z]+$"))
                            return "Last name must start with uppercase and rest lowercase letters";
                        break;

                    case nameof(Username):
                        if (string.IsNullOrEmpty(Username))
                            return "Username is required";
                        if (Username.Length < 6)
                            return "Username must be at least 6 characters";
                        if (!Regex.IsMatch(Username, @"^[a-z.]+$"))
                            return "Username can only contain lowercase letters and dots";
                        if (_context.Employees.Any(e => e.Username == Username) && _currentEmployee.Username!=Username)
                            return "This username already exists";
                        break;

                    case nameof(Password):
                        if (string.IsNullOrEmpty(Password))
                            return "Password is required";
                        if (Password.Length < 6)
                            return "Password must be at least 6 characters";
                        if (!Regex.IsMatch(Password, @"([a-z]|[A-Z]|[0-9]|[!#$])+"))
                            return "Password must contain uppercase, lowercase, number and ! # $";
                        break;

                    case nameof(PasswordRepeat):
                        if (Password != PasswordRepeat)
                            return "Passwords do not match";
                        break;
                }
                return null;
            }
        }
        public string Error => null;

        protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
}
