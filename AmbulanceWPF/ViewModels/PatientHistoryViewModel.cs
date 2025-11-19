// Modified PatientHistoryViewModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AmbulanceWPF.Views;
using System.Collections.ObjectModel;
using AmbulanceWPF.Models;
using AmbulanceWPF.Services;
using AmbulanceWPF.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace AmbulanceWPF.ViewModels
{
    public class PatientHistoryViewModel : INotifyPropertyChanged
    {

        private AmbulanceDbContext _context = new AmbulanceDbContext();
        private MedicalRecord _history;
        private Employee _currentEmployee;

        public MedicalRecord? History
        {
            get => _history;
            set
            {
                _history = value;
                OnPropertyChanged();
            }
        }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }

        // Services (inject via constructor in real app)
        private readonly IPatientService _patientService; // Implement this interface for DB access

        public PatientHistoryViewModel()
        {
            InitializeCommands();
            // For design-time or default
            // Placeholder; use DI in real app
        }

        public PatientHistoryViewModel(Patient patient, Employee currentEmployee) : this()
        {
            Patient = patient;
           _currentEmployee = currentEmployee;
            LoadMedicalRecordAsync(); // Load data
        }

        // Commands
        public ICommand ShowDetailedDiagnosisCommand { get; private set; }
        public ICommand AddExaminationCommand { get; private set; }
        public ICommand AddReferralCommand { get; private set; }
        public ICommand AddDiagnosisCommand { get; private set; }

        private void InitializeCommands()
        {
            AddExaminationCommand = new AsyncRelayCommand(AddNewExaminationAsync);
            AddReferralCommand = new RelayCommand(AddNewReferral);
            AddDiagnosisCommand = new RelayCommand(AddNewDiagnosis);
        }

        private async void LoadMedicalRecordAsync()
        {
            if (Patient == null) return;
            try
            {
                // Fetch from service (DB/API)
                History = await _context.MedicalRecords
                .Include(mr => mr.Patient)
                    .ThenInclude(p => p.ResidenceLocation)
                .Include(mr => mr.Examinations) // Load Examinations collection
                    .ThenInclude(e => e.Employee) // If Employee is a navigation property for doctor details
                .Include(mr => mr.Referrals).ThenInclude(e => e.Employee) // Add for other tabs if needed
                .Include(mr => mr.Diagnoses).ThenInclude(e => e.Employee) // Add for other tabs if needed
                .Where(mr => mr.PatientJMB == Patient.JMB)
                .FirstOrDefaultAsync();

                // If no record, create a new one or handle
                if (History == null)
                {
                    History = new MedicalRecord { PatientJMB = Patient.JMB /* set other defaults */ };
                    //TODO Error control ili neka poruka
                }
            }
            catch (Exception ex)
            {
                // Error handling: Log or show message
                MessageBox.Show($"Error loading medical record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        

        private async Task AddNewExaminationAsync()
        {
            var view = new ExaminationView(_patient, _currentEmployee)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            bool? result = view.ShowDialog();
            if (result == true)
            {
               // await LoadInterventionsAsync(); // Refresh
            }

        }

        private void AddNewReferral()
        {
            var newReferral = new Referral { /*PatientJMB = Patient.JMB  defaults */ };
            /*var addWindow = new AddReferralWindow(newReferral) 
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (addWindow.ShowDialog() == true)
            {
                _patientService.SaveReferralAsync(newReferral);
                History.Referrals.Add(newReferral);
                OnPropertyChanged(nameof(History));
            }*/
        }

        private void AddNewDiagnosis()
        {/*
            var newDiagnosis = new Diagnosis { PatientJMB = Patient.JMB };
            var addWindow = new AddDiagnosisWindow(newDiagnosis)  
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (addWindow.ShowDialog() == true)
            {
                _patientService.SaveDiagnosisAsync(newDiagnosis);
                History.Diagnoses.Add(newDiagnosis);
                OnPropertyChanged(nameof(History));
            }*/
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}