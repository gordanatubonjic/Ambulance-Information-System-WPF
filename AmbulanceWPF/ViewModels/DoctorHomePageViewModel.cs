using AmbulanceWPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AmbulanceWPF.Helper;
using AmbulanceWPF.Models;
using AmbulanceWPF.Views;
using System.Windows;
using AmbulanceWPF.Views.UserControls;
using System.Diagnostics;
using AmbulanceWPF.Data;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceWPF.ViewModels
{
    public class DoctorHomePageViewModel : INotifyPropertyChanged
    {
        private string _searchText = string.Empty;
        private Employee CurrentUser { get; set; }
        private ObservableCollection<Patient> _allPatients;
        private ObservableCollection<Patient> _filteredPatients;
        private ObservableCollection<MedicalRecord> _allRecords;
        private ObservableCollection<Intervention> _interventions;
        private ObservableCollection<MedicationCatalog> _medications;
        public ObservableCollection<MedicationCatalog> Medications => _medications;
        public ObservableCollection<MedicalRecord> AllRecords => _allRecords;


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
            //Employee e = EmployeeRepository.GetEmployee("markic");
            //TODO Fix this employee assignment
            Employee e = new Employee("markic", "markic");//= DummyDataGenerator.GenerateEmployees()[2];
            this.CurrentUser = e;
            LoadPatients(); 
            LoadInterventions();
            if(_allPatients != null)
            FilteredPatients = new ObservableCollection<Patient>(_allPatients);

            ViewPatientCommand = new RelayCommand<Patient>(ViewPatient);
            NavigateToInterventionsCommand = new RelayCommand(ViewInterventions);
            NavigateToPatientsCommand = new RelayCommand(ViewPatients);
            NavigateToProfileCommand = new RelayCommand(ViewProfile);
            NewInterventionCommand = new RelayCommand(NewIntervention);
            HeaderHomeCommand = new RelayCommand(GoHome);
            HeaderLogoutCommand = new RelayCommand(Logout);
            HeaderThemeCommand = new RelayCommand(ChangeTheme);
            HeaderViewProfileCommand = new RelayCommand(ViewProfile);
            OpenAddMedicationCommand = new RelayCommand(OpenAddMedication);

            CurrentContentView = new PatientOverView();


        }

        public DoctorHomePageViewModel(Employee currentUser)
        {
            this.CurrentUser = currentUser;
            LoadPatients();
            LoadInterventions();
            
            if(_allPatients !=null)
            FilteredPatients = new ObservableCollection<Patient>(_allPatients);
            //AllRecords = new ObservableCollection<MedicalRecord>(_allRecords);
            ViewPatientCommand = new RelayCommand<Patient>(ViewPatient);
            NavigateToInterventionsCommand = new RelayCommand(ViewInterventions);
            NavigateToPatientsCommand = new RelayCommand(ViewPatients);
            NewInterventionCommand = new RelayCommand(NewIntervention);
            HeaderHomeCommand = new RelayCommand(GoHome);
            HeaderLogoutCommand = new RelayCommand(Logout);
            HeaderThemeCommand = new RelayCommand(ChangeTheme);
            HeaderViewProfileCommand = new RelayCommand(ViewProfile);
            OpenAddMedicationCommand = new RelayCommand(OpenAddMedication);

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
                 public ICommand HeaderHomeCommand { get; }
        public ICommand HeaderLogoutCommand { get; }
        public ICommand HeaderThemeCommand { get; }
        public ICommand HeaderViewProfileCommand { get; }
        public ICommand OpenAddMedicationCommand { get; }


        private void LoadPatients()
        {
            using (var context = new AmbulanceDbContext())
            {
                var medicalRecords = context.MedicalRecords
                    .Include(mr => mr.Patient)
                        .ThenInclude(p => p.ResidenceLocation)
                    .Where(mr => mr.DoctorJMB == CurrentUser.JMB)
                    .ToList();

                _allPatients = new ObservableCollection<Patient>(
                    medicalRecords.Select(mr => mr.Patient)
                );
            }
        }
        private void LoadInterventions()
        {
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
                p.LastName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
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

                                 interventionView.ShowDialog();

                                 LoadInterventions();
            
        }

        private void OpenAddMedication()
        {
            var addMedicationView = new AddMedicationView();
            addMedicationView.Owner = Application.Current.Windows.OfType<InterventionView>().FirstOrDefault();
            addMedicationView.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                         bool? result = addMedicationView.ShowDialog();

            if (result == true)
            {
                                              }
        }


        private void GoHome()
        {
                         CurrentContentView = new PatientOverView();
            Console.WriteLine("Home button clicked");
        }

        private void Logout()
        {
                         foreach (Window window in Application.Current.Windows)
            {
                if (window is DoctorHomePageView)
                {
                    window.Close();
                    break;
                }
            }
            var loginWindow = new LoginFormView();
            loginWindow.Show();
        }

        private void ChangeTheme()
        {
                         var currentTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Themes/"));

            if (currentTheme != null && currentTheme.Source.OriginalString.Contains("Light"))
            {
                AppTheme.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
            }
            else
            {
                AppTheme.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
            }
        }


        public string DoctorName => CurrentUser?.Name + " " + CurrentUser?.LastName;
        public string DoctorInitials => GetInitials(CurrentUser);
        public string DoctorEmail => CurrentUser?.Username + "@ambulance.com";
        public string DoctorRole => CurrentUser?.Role;

        private string GetInitials(Employee employee)
        {
            if (employee == null || string.IsNullOrEmpty(employee.Name) || string.IsNullOrEmpty(employee.LastName))
                return "??";

            return $"{employee.Name[0]}{employee.LastName[0]}".ToUpper();
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


