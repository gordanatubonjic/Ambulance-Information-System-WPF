using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AmbulanceWPF.Helper;
using AmbulanceWPF.Repository;
using AmbulanceWPF.Models;
using AmbulanceWPF.Views;
using System.Windows;

namespace AmbulanceWPF.ViewModels
{
    public class DoctorHomePageViewModel : INotifyPropertyChanged
    {
        private string _searchText = string.Empty;

        private Doctor currentUser { get; set; }

        private ObservableCollection<Patient> _allPatients;
        private ObservableCollection<Patient> _filteredPatients;
        private ObservableCollection<Intervention> _interventions;



        public DoctorHomePageViewModel(Doctor currentUser)
        {
            LoadPatients(); // Load your patients from database/service
            LoadInterventions();
            FilteredPatients = new ObservableCollection<Patient>(_allPatients);
            ViewPatientCommand = new RelayCommand<Patient>(ViewPatient);
            NavigateToInterventionsCommand = new RelayCommand<Intervention>(ViewInterventions);
            this.currentUser = currentUser;
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

        public ICommand NavigateToInterventionsCommand { get; }

        private void LoadPatients()
        {
            // Replace with actual data loading logic
            _allPatients = new ObservableCollection<Patient>(PatientRepository.GetPatients());
        }
        private void LoadInterventions() {
            _interventions = new ObservableCollection<Intervention>(InterventionRepository.GetInterventions(currentUser.JMBG));
        }

        private void FilterPatients()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredPatients = new ObservableCollection<Patient>(_allPatients);
                return;
            }

            var filtered = _allPatients.Where(p =>
                p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Surname.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.JMB.Contains(SearchText, StringComparison.OrdinalIgnoreCase) )
                .ToList();

            FilteredPatients = new ObservableCollection<Patient>(filtered);
        }

        private void ViewPatient(Patient patient)
        {
            // Open Patient History View when a patient is selected
            var patientHistoryView = new PatientHistoryView(new PatientHistoryViewModel(patient));
            patientHistoryView.Owner = Application.Current.MainWindow;
            patientHistoryView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            patientHistoryView.Show();
        }

        private void ViewInterventions() {
           //TODO change current view so that interventions are shown in Grid.Row=1 and Grid.Col=1
           //it should present information about interventions in rows where each row has data from Intervention 
           //object
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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