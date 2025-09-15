using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AmbulanceWPF.Models;


namespace AmbulanceWPF.ViewModels
{
        public class ProfileViewModel : INotifyPropertyChanged, IDataErrorInfo
        {
            private Employee _currentEmployee;
            private string _name;
            private string _surname;
            private string _username;
            private string _password;
            private bool _isValid;

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

            public string Surname
            {
                get => _surname;
                set
                {
                    _surname = value;
                    OnPropertyChanged(nameof(Surname));
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
            // Load current user data (you'll need to pass the current user)
                _currentEmployee = e;
                LoadCurrentUser(e);

                SaveCommand = new RelayCommand(SaveChanges, CanSaveChanges);
                CancelCommand = new RelayCommand(CancelChanges);
            }

            private void LoadCurrentUser(Employee _currentEmployee)
            {
                // TODO: Replace with actual current user loading logic
                // For now, using a sample or getting from repository
               
                if (_currentEmployee != null)
                {
                    Name = _currentEmployee.Name;
                    Surname = _currentEmployee.Surname;
                    Username = _currentEmployee.Username;
                    Password = _currentEmployee.Password;
                }
            }

            private void SaveChanges()
            {
                if (!IsValid) return;

                try
                {
                    // Update the employee object
                    _currentEmployee.Name = Name;
                    _currentEmployee.Surname = Surname;
                    _currentEmployee.Username = Username;
                    _currentEmployee.Password = Password;

                    // TODO: Add repository method to update employee
                    // EmployeeRepository.UpdateEmployee(_currentEmployee);

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
                // Reset to original values
              //  LoadCurrentUser();
            }

            private void Validate()
            {
                IsValid = !string.IsNullOrEmpty(Name) &&
                         !string.IsNullOrEmpty(Surname) &&
                         !string.IsNullOrEmpty(Username) &&
                         !string.IsNullOrEmpty(Password) &&
                         Username.Length >= 6 &&
                         Password.Length >= 6;
            }

            #region IDataErrorInfo Implementation
            public string this[string columnName]
            {
                get
                {
                    switch (columnName)
                    {
                        case nameof(Username):
                            if (string.IsNullOrEmpty(Username))
                                return "Username is required";
                            if (Username.Length < 6)
                                return "Username must be at least 6 characters";
                            break;

                        case nameof(Password):
                            if (string.IsNullOrEmpty(Password))
                                return "Password is required";
                            if (Password.Length < 6)
                                return "Password must be at least 6 characters";
                            break;

                        case nameof(Name):
                            if (string.IsNullOrEmpty(Name))
                                return "First name is required";
                            break;

                        case nameof(Surname):
                            if (string.IsNullOrEmpty(Surname))
                                return "Last name is required";
                            break;
                    }
                    return null;
                }
            }

            public string Error => null;
            #endregion

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
}
