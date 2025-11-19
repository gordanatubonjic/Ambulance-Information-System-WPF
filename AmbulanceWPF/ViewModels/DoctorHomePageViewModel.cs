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
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mysqlx.Crud;
using System.Globalization;
using Google.Protobuf.WellKnownTypes;

namespace AmbulanceWPF.ViewModels
{
    public class DoctorHomePageViewModel : INotifyPropertyChanged
    {
        private string _searchText = string.Empty;
        private Employee _currentUser;
        public Employee CurrentUser
        {
            get => _currentUser!;
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChanged(nameof(CurrentUser));

                }
            }
        }

private object _currentContentView;
        

        private AmbulanceDbContext _context = new AmbulanceDbContext();
        private ObservableCollection<Patient> _allPatients;
        private ObservableCollection<Patient> _filteredPatients;
        private ObservableCollection<MedicalRecord> _allRecords;
        private ObservableCollection<Intervention> _interventions;
        private ObservableCollection<MedicationCatalog> _medications;

        public ObservableCollection<MedicationCatalog> Medications => _medications;
        public ObservableCollection<MedicalRecord> AllRecords => _allRecords;

        private ProfileViewModel profileViewModel;
        public ObservableCollection<string> AvailableThemes { get; set; }
        public ObservableCollection<string> AvailableLanguages { get; set; }

        private string? _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme!;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();
                    
                    //IsThemeComboBoxVisible = false;
                }
            }
        }
        private string? _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage!;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged();
                    
                    //IsLanguageComboBoxVisible = false;
                }
            }
        }
        public Patient Patient
        {
            get;
            set;
        }
        public DoctorHomePageViewModel(Employee currentUser)
        {
            this.CurrentUser = currentUser;
            AvailableThemes = new ObservableCollection<string> { "Light", "Blue", "Dark" };
            AvailableLanguages = new ObservableCollection<string> { "English", "Serbian" };
            SelectedTheme = SessionManager.CurrentTheme
                            ?? CurrentUser.Theme
                            ?? "Light";
            SelectedLanguage = SessionManager.CurrentLanguage
                               ?? CurrentUser.Language
                               ?? "English";

            _currentThemeIndex = AvailableThemes.IndexOf(SelectedTheme);
            if (_currentThemeIndex < 0) _currentThemeIndex = 0;
            _currentLanguageIndex = AvailableLanguages.IndexOf(SelectedLanguage);
            if (_currentLanguageIndex < 0) _currentLanguageIndex = 0;
            HeaderThemeCommand = new AsyncRelayCommand(CycleTheme);
            HeaderLanguageCommand = new AsyncRelayCommand(CycleLanguage);
            LoadPatients();
            LoadInterventions();

            if (_allPatients != null)
                FilteredPatients = new ObservableCollection<Patient>(_allPatients);

            NavigateToInterventionsCommand = new AsyncRelayCommand(NavigateToInterventionsAsync);
            NavigateToPatientsCommand = new AsyncRelayCommand(NavigateToPatientsAsync);
            NavigateToProfileCommand = new AsyncRelayCommand(NavigateToProfileAsync);
            NewInterventionCommand = new AsyncRelayCommand(NewInterventionAsync);
            NewExamCommand = new AsyncRelayCommand(NewExaminationAsync);
            MonthlyReportCommand = new AsyncRelayCommand(MonthlyReportCommandAsync);
            OpenAddMedicationCommand = new AsyncRelayCommand(OpenAddMedicationAsync);
            HeaderHomeCommand = new AsyncRelayCommand(HeaderHomeAsync);
            HeaderLogoutCommand = new AsyncRelayCommand(HeaderLogoutAsync);
            HeaderViewProfileCommand = new AsyncRelayCommand(NavigateToProfileAsync);
            MedicalRecordDisplayCommand = new RelayCommand<Patient>(NavigateToMedicalRecord);
            CurrentContentView = new PatientOverView();
            InitializeAsync();


        }
        private async Task InitializeAsync()
        {
            await LoadPatientsAsync();
            await LoadInterventionsAsync();
            FilteredPatients = new ObservableCollection<Patient>(_allPatients ?? new ObservableCollection<Patient>());
            CurrentContentView = new PatientOverView();
            //await LoadAndApplyInitialThemeAsync();
        }
        private async Task CycleTheme()
        {
            if (AvailableThemes == null || AvailableThemes.Count == 0)
                return;

            _currentThemeIndex = (_currentThemeIndex + 1) % AvailableThemes.Count;
            _selectedTheme = AvailableThemes[_currentThemeIndex];
            ApplyTheme(_selectedTheme);
        }
        private async Task CycleLanguage()
        {
            if (AvailableLanguages == null || AvailableLanguages.Count == 0)
                return;

            _currentLanguageIndex = (_currentLanguageIndex + 1) % AvailableLanguages.Count;

            string langCode = AvailableLanguages[_currentLanguageIndex];
            SelectedLanguage = langCode;
            await ApplyLanguage(langCode);
        }
        private async Task ApplyTheme(string themeName)
        {
            SessionManager.CurrentTheme = themeName;
            string themePath = themeName switch
            {
                "Light" => "Themes/Light.xaml",
                "Blue" => "Themes/Blue.xaml",
                "Dark" => "Themes/Dark.xaml",
                _ => "Themes/Light.xaml"
            };
            var themeDict = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };
            var materialDesignDefaults = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml")
            };
            for (int i = Application.Current.Resources.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var md = Application.Current.Resources.MergedDictionaries[i];
                if (md.Source != null &&
                    (md.Source.OriginalString.Contains("Theme") ||
                     md.Source.OriginalString.Contains("Styles.xaml") ||
                     md.Source.OriginalString.Contains("MaterialDesign3.Defaults.xaml")))
                {
                    Application.Current.Resources.MergedDictionaries.Remove(md);
                }
            }
            Application.Current.Resources.MergedDictionaries.Add(materialDesignDefaults);
            Application.Current.Resources.MergedDictionaries.Add(themeDict);
            using var context = new AmbulanceDbContext();
            var userInDb = context.Employees.FirstOrDefault(u => u.JMB == _currentUser.JMB);
            if (userInDb != null)
            {
                userInDb.Theme = themeName;
                context.SaveChanges();
            }
        }
        private async Task ApplyLanguage(string langCode)
        {
            SessionManager.CurrentLanguage = langCode;
            string dictionaryPath = langCode == "Serbian"
                ? "LanguageDictionaries/SerbianDictionary.xaml"
                : "LanguageDictionaries/EnglishDictionary.xaml";
            var newDict = new ResourceDictionary
            {
                Source = new Uri(dictionaryPath, UriKind.Relative)
            };
            for (int i = Application.Current.Resources.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var md = Application.Current.Resources.MergedDictionaries[i];
                if (md.Source != null && md.Source.OriginalString.Contains("LanguageDictionaries"))
                {
                    Application.Current.Resources.MergedDictionaries.Remove(md);
                }
            }
            Application.Current.Resources.MergedDictionaries.Add(newDict);

            using var context = new AmbulanceDbContext();
            var userInDb = context.Employees.FirstOrDefault(u => u.JMB == _currentUser.JMB);
            if (userInDb != null)
            {
                userInDb.Language = langCode;
                context.SaveChanges();
            }
        }
        private async Task LoadPatientsAsync()
        {
            var medicalRecords = await _context.MedicalRecords
                .Include(mr => mr.Patient)
                    .ThenInclude(p => p.ResidenceLocation)
                .Where(mr => mr.DoctorJMB == CurrentUser.JMB)
                .ToListAsync();

            _allPatients = new ObservableCollection<Patient>(medicalRecords.Select(mr => mr.Patient).Distinct());
        }
        private async Task NavigateToInterventionsAsync()
        {
            CurrentContentView = new InterventionsContents(_interventions);
        }
        private async Task NavigateToPatientsAsync()
        {
            CurrentContentView = new PatientOverView();
        }
        private async Task NavigateToProfileAsync()
        {
            CurrentContentView = new ProfileView(CurrentUser);
        }
        private void NavigateToMedicalRecord(Patient patient)
        {
            if (patient == null) return;
            var view = new PatientHistoryView(patient, CurrentUser)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            bool? result = view.ShowDialog();
            if (result == true)
            {
                //Treba nesto da se desi
            }


        }
        private async Task OpenAddMedicationAsync()
        {
            var view = new AddMedicationView
            {
                Owner = Application.Current.Windows.OfType<InterventionView>().FirstOrDefault() ?? Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            bool? result = view.ShowDialog();
            if (result == true)
            {
                // Refresh medications if needed
            }
        }
        private async Task HeaderHomeAsync()
        {
            CurrentContentView = new PatientOverView();
            Console.WriteLine("Home button clicked");
        }
        private async Task HeaderLogoutAsync()
        {
            // Create the login window first
            var loginWindow = new LoginFormView();

            // Close all existing windows except the login window
            foreach (Window window in Application.Current.Windows.OfType<Window>().ToList())
            {
                if (window != loginWindow)
                {
                    window.Close();
                }
            }

            // Show the login window
            loginWindow.Show();

            // Set it as main window if needed
            Application.Current.MainWindow = loginWindow;
        }
        private void FilterPatients()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredPatients = new ObservableCollection<Patient>(_allPatients ?? new ObservableCollection<Patient>());
                return;
            }

            var filtered = _allPatients?.Where(p =>
                p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.LastName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.JMB.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList() ?? new List<Patient>();

            FilteredPatients = new ObservableCollection<Patient>(filtered);
        }
        public string DoctorName => $"{CurrentUser?.Name} {CurrentUser?.LastName}";
        public string DoctorInitials => GetInitials(CurrentUser);
        public string DoctorEmail => $"{CurrentUser?.Username}@ambulance.com";
        public string DoctorRole => CurrentUser?.Role;
        private string GetInitials(Employee employee)
        {
            return employee == null || string.IsNullOrEmpty(employee.Name) || string.IsNullOrEmpty(employee.LastName)
                ? "??"
                : $"{employee.Name[0]}{employee.LastName[0]}".ToUpper();
        }
        public object CurrentContentView
        {
            get => _currentContentView;
            set
            {
                _currentContentView = value;
                OnPropertyChanged();
            }
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
        public ICommand NavigateToProfileCommand { get; }
        public ICommand NewInterventionCommand { get; }
        public ICommand NewExamCommand { get; }
        public ICommand MonthlyReportCommand { get; }
        public ICommand HeaderHomeCommand { get; }
        public ICommand HeaderLogoutCommand { get; }
        public ICommand HeaderThemeCommand { get; }
        public ICommand HeaderLanguageCommand { get; }
        public ICommand HeaderViewProfileCommand { get; }
        public ICommand OpenAddMedicationCommand { get; }
        public ICommand MedicalRecordDisplayCommand { get; }

        private int _currentThemeIndex;
        private int _currentLanguageIndex;
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
            var interventions = _context.Interventions
                .Include(i => i.Patient)
                    .ThenInclude(p => p.ResidenceLocation) // Include patient location if needed
                .Include(i => i.InterventionDoctors)
                    .ThenInclude(id => id.Employee) // Include doctor details if needed
                .Include(i => i.Therapies) // Include therapies if needed
                    .ThenInclude(t => t.Medication)
                .Where(i => i.InterventionDoctors.Any(id => id.DoctorJMB == CurrentUser.JMB))
                .OrderByDescending(i => i.Date) // Changed from DateTime to Date
                .ToList();

            _interventions = new ObservableCollection<Intervention>(interventions);
        }
        private void ViewInterventions()
        {
            CurrentContentView = new InterventionsContents(_interventions);
        }
        private void ViewPatients()
        {
            CurrentContentView = new PatientOverView();
        }
     
        private void ViewPatient(Patient patient)
        {
            var patientHistoryView = new PatientHistoryView(new PatientHistoryViewModel(patient, _currentUser));
            patientHistoryView.Owner = Application.Current.MainWindow;
            patientHistoryView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            patientHistoryView.Show();
        }
        private void ViewProfile()
        {
            CurrentContentView = new ProfileView(CurrentUser);
        }

        //TODO Da li je potrebno asinhrone sve komande imati
        private void NewIntervention()
        {

            var interventionView = new InterventionView(CurrentUser);
            interventionView.Owner = Application.Current.MainWindow;
            interventionView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            interventionView.ShowDialog();

        }
        private async Task NewInterventionAsync()
        {
            var view = new InterventionView(CurrentUser)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            bool? result = view.ShowDialog();
            if (result == true)
            {
                await LoadInterventionsAsync(); // Refresh
            }
        }
        private async Task NewExaminationAsync()
        {
            var view = new ExaminationView(CurrentUser)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            bool? result = view.ShowDialog();
            if (result == true)
            {
                //await LoadExaminationsAsync(); // Refresh
            }

        }
        private async Task MonthlyReportCommandAsync()
        {
            var window = new AmbulanceWPF.Views.MonthlyReportView();
            window.ShowDialog(); // Or window.Show() for non-modal
        }


        private async Task LoadInterventionsAsync()
        {
            var interventions = await _context.Interventions
                .Include(i => i.Patient)
                    .ThenInclude(p => p.ResidenceLocation) // Include patient location if needed
                .Include(i => i.InterventionDoctors)
                    .ThenInclude(id => id.Employee) // Include doctor details if needed
                .Include(i => i.Therapies) // Include therapies if needed
                    .ThenInclude(t => t.Medication)
                .Where(i => i.InterventionDoctors.Any(id => id.DoctorJMB == _currentUser.JMB))
                .OrderByDescending(i => i.Date) // Changed from DateTime to Date
                .ToListAsync();

            _interventions = new ObservableCollection<Intervention>(interventions);
        }
        private void OpenAddMedication()
        {
            var addMedicationView = new AddMedicationView();
            addMedicationView.Owner = Application.Current.Windows.OfType<InterventionView>().FirstOrDefault();
            addMedicationView.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            bool? result = addMedicationView.ShowDialog();

            if (result == true)
            { //TODO
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


