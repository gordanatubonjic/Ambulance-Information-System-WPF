using AmbulanceWPF;
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
using AmbulanceWPF.Views.UserControls;
using System.Diagnostics;

namespace AmbulanceWPF.ViewModels
{
    public class DoctorHomePageViewModel : INotifyPropertyChanged
    {
        private string _searchText = string.Empty;
        private Employee CurrentUser { get; set; }
        private ObservableCollection<Patient> _allPatients;
        private ObservableCollection<Patient> _filteredPatients;
        private ObservableCollection<Intervention> _interventions;

        private ProfileViewModel profileViewModel;


        private object _currentContentView;
        public object CurrentContentView
        {
            get => _currentContentView;
            set
            {
                _currentContentView = value;
                OnPropertyChanged();
            }
        }

        public DoctorHomePageViewModel() {
            Employee e = EmployeeRepository.GetEmployee("markic");
            this.CurrentUser = e;
            LoadPatients(); // Load your patients from database/service
            LoadInterventions();
            if(_allPatients != null)
            FilteredPatients = new ObservableCollection<Patient>(_allPatients);

            ViewPatientCommand = new RelayCommand<Patient>(ViewPatient);
            NavigateToInterventionsCommand = new RelayCommand(ViewInterventions);
            NavigateToPatientsCommand = new RelayCommand(ViewPatients);
            NavigateToProfileCommand = new RelayCommand(ViewProfile);
            NewInterventionCommand = new RelayCommand(NewIntervention);
            CurrentContentView = new PatientOverView();


        }

        public DoctorHomePageViewModel(Employee currentUser)
        {
            this.CurrentUser = currentUser;
            LoadPatients(); // Load your patients from database/service
            LoadInterventions();
            if(_allPatients !=null)
            FilteredPatients = new ObservableCollection<Patient>(_allPatients);

            ViewPatientCommand = new RelayCommand<Patient>(ViewPatient);
            NavigateToInterventionsCommand = new RelayCommand(ViewInterventions);
            NavigateToPatientsCommand = new RelayCommand(ViewPatients);
            NewInterventionCommand = new RelayCommand(NewIntervention);
            CurrentContentView = new PatientOverView();

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

        public ObservableCollection<Intervention> Interventions => _interventions;

        public bool IsEmptyStateVisible => !FilteredPatients?.Any() ?? true && !string.IsNullOrWhiteSpace(SearchText);

        public ICommand ViewPatientCommand { get; }
        public ICommand NavigateToInterventionsCommand { get; }
        public ICommand NavigateToPatientsCommand { get; }
        public ICommand NavigateToProfileCommand { get;  }
        public ICommand NewInterventionCommand { get; }

        private void LoadPatients()
        {
            // Replace with actual data loading logic
            _allPatients = new ObservableCollection<Patient>(PatientRepository.GetPatients());
        }
        private void LoadInterventions()
        {
            _interventions = new ObservableCollection<Intervention>(
                InterventionRepository.GetInterventions(CurrentUser.JMBG));
        }

        private void ViewInterventions()
        {
            
             CurrentContentView = new InterventionsContents();
        }

        private void ViewPatients()
        {
            CurrentContentView = new PatientOverView();
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
                p.JMB.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredPatients = new ObservableCollection<Patient>(filtered);
        }

        private void ViewPatient(Patient patient)
        {
            var patientHistoryView = new PatientHistoryView(new PatientHistoryViewModel(patient));
            patientHistoryView.Owner = Application.Current.MainWindow;
            patientHistoryView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            patientHistoryView.Show();
        }
        private void ViewProfile()
        {
            CurrentContentView = new ProfileView(CurrentUser);
        }

        private void NewIntervention() {
            
                var interventionView = new InterventionView(CurrentUser);
                interventionView.Owner = Application.Current.MainWindow;
                interventionView.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                // Show as modal dialog
                interventionView.ShowDialog();

                // Refresh interventions after the dialog closes
                LoadInterventions();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
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


