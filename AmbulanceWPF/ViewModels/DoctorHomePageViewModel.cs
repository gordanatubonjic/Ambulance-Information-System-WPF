using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AmbulanceWPF.Helper;

namespace AmbulanceWPF.ViewModels
{
    public class DoctorHomePageViewModel : INotifyPropertyChanged
    {
        private string _searchText = string.Empty;
        private ObservableCollection<Patient> _allPatients;
        private ObservableCollection<Patient> _filteredPatients;

        public DoctorHomePageViewModel()
        {
            LoadPatients(); // Load your patients from database/service
            FilteredPatients = new ObservableCollection<Patient>(_allPatients);
            ViewPatientCommand = new RelayCommand<Patient>(ViewPatient);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterPatients();
            }
        }

        public ObservableCollection<Patient> FilteredPatients
        {
            get => _filteredPatients;
            set
            {
                _filteredPatients = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmptyStateVisible));
            }
        }

        public bool IsEmptyStateVisible => !FilteredPatients?.Any() ?? true && !string.IsNullOrWhiteSpace(SearchText);

        public ICommand ViewPatientCommand { get; }

        private void LoadPatients()
        {
            // Replace with actual data loading logic
            _allPatients = new ObservableCollection<Patient>
            {
                new Patient
                {
                    PatientId = "P001",
                    FirstName = "John",
                    LastName = "Doe",
                    Age = 45,
                    Gender = "Male",
                    PhoneNumber = "+387 65 123 456",
                    Address = "Banja Luka, Srpska",
                    LastVisit = DateTime.Now.AddDays(-5),
                    Priority = "High"
                },
                new Patient
                {
                    PatientId = "P002",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Age = 32,
                    Gender = "Female",
                    PhoneNumber = "+387 66 789 012",
                    Address = "Sarajevo, BiH",
                    LastVisit = DateTime.Now.AddDays(-12),
                    Priority = "Medium"
                },
                new Patient
                {
                    PatientId = "P003",
                    FirstName = "Marko",
                    LastName = "Petrović",
                    Age = 28,
                    Gender = "Male",
                    PhoneNumber = "+387 65 345 678",
                    Address = "Doboj, Srpska",
                    LastVisit = DateTime.Now.AddDays(-3),
                    Priority = "Low"
                }
            };
        }

        private void FilterPatients()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredPatients = new ObservableCollection<Patient>(_allPatients);
                return;
            }

            var filtered = _allPatients.Where(p =>
                p.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.PatientId.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.PhoneNumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Address.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredPatients = new ObservableCollection<Patient>(filtered);
        }

        private void ViewPatient(Patient patient)
        {
            // Handle patient selection - navigate to detail view, etc.
            // Example: NavigationService.NavigateToPatientDetail(patient.PatientId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class Patient
    {
        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime LastVisit { get; set; }
        public string Priority { get; set; }
    }

    // Simple RelayCommand implementation
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? true;
        public void Execute(object parameter) => _execute((T)parameter);
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}


















/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace AmbulanceWPF.ViewModels
{
    public class DoctorHomePageViewModel
    {

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                // Optionally trigger search live
                //ExecuteSearch();
            }
        }

        public ICommand SearchCommand { get; }

        public DoctorHomePageViewModel()
        {
               SearchCommand = new RelayCommand(ExecuteSearch);
            
               // SearchCommand = new RelayCommand(() => MessageBox.Show($"Search for: {SearchText}"));
            

        }

        private void ExecuteSearch()
        {
            // Implement your filtering/search logic here
            MessageBox.Show($"Searching for: {SearchText}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
*/