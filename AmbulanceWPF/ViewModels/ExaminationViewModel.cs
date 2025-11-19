using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbulanceWPF.Data;
using AmbulanceWPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AmbulanceWPF.Views;
using System.Numerics;

namespace AmbulanceWPF.ViewModels
{
    public class ExaminationViewModel : ViewModelBase
    {
        private readonly AmbulanceDbContext _context;
        private Patient _selectedPatient;
        private readonly Employee _doctor;
        private Examination _examination;
        private string _diagnosisOpinion;
        private DiseaseCatalog _selectedDisease;
        private string _referralSpecialists;
        private DiseaseCatalog _selectedReferralDisease;
        private decimal _procurementQuantity;
        private string _patientFirstName;
        private string _patientLastName;
        private string _patientJMB;
        private string _patientLocationName;
        private string _allergies;
        private string _specialistsText;



        public ObservableCollection<DiseaseCatalog> Diseases { get; } = new();
        public ObservableCollection<Diagnosis> Diagnosis { get; } = new();
        public ObservableCollection<Referral> Referrals { get; } = new();
        public ObservableCollection<Procurement> Procurements { get; } = new();
        public string? PatientDateOfBirth { get; set; }
        public ObservableCollection<string> AvailableLocations { get; set; }

        public string SelectedLocation { get; set; }

        public string SpecialistsText
        {
            get => _specialistsText;
            set { _specialistsText = value; OnPropertyChanged(nameof(SpecialistsText)); }
        }
        private string _referalReasonText;
        public string ReferalReasonText
        {
            get => _referalReasonText;
            set { _referalReasonText = value; OnPropertyChanged(nameof(ReferalReasonText)); }
        }
        public Examination Examination
        {
            get => _examination;
            set { _examination = value; OnPropertyChanged(nameof(Examination)); }
        }
        public int ExaminationId
        {
            get => _examination.ExaminationId;
            set { _examination.ExaminationId = value; OnPropertyChanged(nameof(Examination)); }
        }
        public string PatientFirstName
        {
            get => _patientFirstName;
            set { _patientFirstName = value; OnPropertyChanged(nameof(PatientFirstName)); }
        }

        public string PatientLastName
        {
            get => _patientLastName;
            set { _patientLastName = value; OnPropertyChanged(nameof(PatientLastName)); }
        }

        public string PatientJMB
        {
            get => _patientJMB;
            set { _patientJMB = value; OnPropertyChanged(nameof(PatientJMB)); }
        }
        
        public string PatientLocationName
        {
            get { return _patientLocationName; }
            set { _patientLocationName = value; OnPropertyChanged(nameof(PatientLocationName)); }
        }

        public string Allergies
        {
            get => _allergies;
            set { _allergies = value; OnPropertyChanged(nameof(Allergies)); }
        }
        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                if (value != null)
                {
                    PatientFirstName = value.Name;
                    PatientLastName = value.LastName;
                    PatientJMB = value.JMB;
                    PatientDateOfBirth = value.DateOfBirth.ToString();
                    Allergies = value.Allergies;
                    SelectedLocation = PatientLocationName = value.ResidenceLocation.Name;

                }
                OnPropertyChanged(nameof(SelectedPatient));
                CreateExamination(_selectedPatient);
            }
        }
        private Examination CreateExamination(Patient patient) {
            _examination = new Examination
            {
                PatientJMB = patient.JMB,
                DoctorJMB = _doctor.JMB,
                ExaminationDate = DateTime.Now
            };
            return _examination;
        }
        public Employee Doctor => _doctor;

        public string DiagnosisOpinion
        {
            get => _diagnosisOpinion;
            set { _diagnosisOpinion = value; OnPropertyChanged(nameof(DiagnosisOpinion)); }
        }

        public DiseaseCatalog SelectedDisease
        {
            get => _selectedDisease;
            set { _selectedDisease = value; OnPropertyChanged(nameof(SelectedDisease)); }
        }

        public int DiseaseCode
        {get;set;
        }

        public string ReferralSpecialists
        {
            get => _referralSpecialists;
            set { _referralSpecialists = value; OnPropertyChanged(nameof(ReferralSpecialists)); }
        }

        public DiseaseCatalog SelectedReferralDisease
        {
            get => _selectedReferralDisease;
            set { _selectedReferralDisease = value; OnPropertyChanged(nameof(SelectedReferralDisease)); }
        }

        public decimal ProcurementQuantity
        {
            get => _procurementQuantity;
            set { _procurementQuantity = value; OnPropertyChanged(nameof(ProcurementQuantity)); }
        }

        public ICommand AddDiagnosisCommand { get; }
        public ICommand RemoveDiagnosisCommand { get; }
        public ICommand AddReferralCommand { get; }
        public ICommand RemoveReferralCommand { get; }
        public ICommand AddProcurementCommand { get; }
        public ICommand RemoveProcurementCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CancelDiagnosisCommand { get; }
        public ICommand CancelReferralCommand { get; }
        public ICommand FindOrCreatePatientCommand { get; }
        private AddDiagnosisView _diagnosisView { get; set; }
        private ReferralView _referralView { get; set; }
        public ExaminationViewModel(Patient patient, Employee doctor, ExaminationView view)
        {
            //if(patient!=null)
            
            _doctor = doctor;
            _context = new AmbulanceDbContext();
            _view = view;
            if (patient != null)
            {
                InitializePatient(patient);
            }

            _examination = CreateExamination(patient);

            LoadDiseasesAsync();

            AddDiagnosisCommand = new RelayCommand(AddDiagnosis, CanAddDiagnosis);
            RemoveDiagnosisCommand = new RelayCommand<Diagnosis>(RemoveDiagnosis);
            AddReferralCommand = new RelayCommand(AddReferral, CanAddReferral);
            RemoveReferralCommand = new RelayCommand<Referral>(RemoveReferral);
            AddProcurementCommand = new RelayCommand(AddProcurement, CanAddProcurement);
            RemoveProcurementCommand = new RelayCommand<Procurement>(RemoveProcurement);
            SaveDiagnosisCommand = new RelayCommand(SaveDiagnosisAsync);
            SaveReferralCommand = new RelayCommand(SaveReferralAsync, CanSave);

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            CancelCommand = new RelayCommand(Cancel);
            CancelDiagnosisCommand = new RelayCommand(CancelDiagnosis);
            CancelReferralCommand = new RelayCommand(CancelReferral);
             
        }
        private void InitializePatient(Patient patient)
        {
            _selectedPatient = patient;
            _examination = CreateExamination(patient);

            // Set UI properties
            PatientFirstName = patient.Name;
            PatientLastName = patient.LastName;
            PatientJMB = patient.JMB;
            PatientDateOfBirth = patient.DateOfBirth.ToString();
            Allergies = patient.Allergies;
            PatientLocationName = patient.ResidenceLocation?.Name;
            SelectedLocation = patient.ResidenceLocation?.Name;
        }
        private readonly ExaminationView _view;

        public ExaminationViewModel(Employee doctor,  ExaminationView view)
        {
            
            _doctor = doctor;
            _context = new AmbulanceDbContext();
            _view = view;
           
            //TODO Moram settovati pacijenta iz prozora

            LoadDiseasesAsync();
            LoadAvailableLocations();
            FindOrCreatePatientCommand = new AsyncRelayCommand(FindOrCreatePatientAsync);

            AddDiagnosisCommand = new RelayCommand(AddDiagnosis, CanAddDiagnosis);
            RemoveDiagnosisCommand = new RelayCommand<Diagnosis>(RemoveDiagnosis);
            AddReferralCommand = new RelayCommand(AddReferral, CanAddReferral);
            RemoveReferralCommand = new RelayCommand<Referral>(RemoveReferral);
            AddProcurementCommand = new RelayCommand(AddProcurement, CanAddProcurement);
            RemoveProcurementCommand = new RelayCommand<Procurement>(RemoveProcurement);
            SaveDiagnosisCommand = new RelayCommand(SaveDiagnosisAsync);
            SaveReferralCommand = new RelayCommand(SaveReferralAsync, CanSave
        );


            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            CancelCommand = new RelayCommand(Cancel);
            CancelDiagnosisCommand = new RelayCommand(CancelDiagnosis);
        }
        private async Task LoadAvailableLocations()
        {
            AvailableLocations = new ObservableCollection<string>(
                await _context.Locations
                    .Select(l => l.Name)
                    .OrderBy(name => name)
                    .ToListAsync()
            );
        }
        private async Task FindOrCreatePatientAsync()
        {
            Patient patient = null;

            using var context = new AmbulanceDbContext();

            if (!string.IsNullOrEmpty(PatientJMB))
            {
                patient = await context.Patients.Include(p => p.ResidenceLocation).FirstOrDefaultAsync(p => p.JMB == PatientJMB);
            }

            if (patient == null && !string.IsNullOrEmpty(PatientFirstName) && !string.IsNullOrEmpty(PatientLastName))
            {
                patient = await context.Patients
                        .Include(p => p.ResidenceLocation)
                        .FirstOrDefaultAsync(p =>
                            p.Name.ToLower() == PatientFirstName.ToLower() &&
                            p.LastName.ToLower() == PatientLastName.ToLower());
            }

            if (patient != null)
            {
                SelectedPatient = patient;
                MessageBox.Show("Patient found.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                MessageBox.Show("Patient not found.", "Unsuccessefull", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           
        }

        private string GenerateUniqueJMB(AmbulanceDbContext context)
        {
            var rand = new Random();
            string jmb;
            do
            {
                jmb = rand.NextInt64(1000000000000, 9999999999999).ToString(); // 13-digit random
            } while (context.Patients.Any(p => p.JMB == jmb));
            return jmb;
        }
        private async Task LoadDiseasesAsync()
        {
            var diseases = await _context.DiseaseCatalogs.ToListAsync();
            foreach (var disease in diseases)
            {
                Diseases.Add(disease);
            }
        }

        public ICommand SaveReferralCommand { get; }
       
        public ICommand SaveDiagnosisCommand { get; }
        private void SaveDiagnosisAsync() {
            if (DiseaseCode == null || string.IsNullOrEmpty(DiagnosisOpinion) ) return;

            Diagnosis.Add(new Diagnosis
            {
                PatientJMB = SelectedPatient.JMB,
                DiseaseCode = DiseaseCode,
                Date = DateTime.Now,
                DoctorOpinion = DiagnosisOpinion,
                ExaminationId = _examination.ExaminationId, // Will be set on save
                DoctorJMB = _doctor.JMB
            });

            CloseDiagnosis();
            //After saving diagnosis to my collection, close AddDiagnosis window
        }
        private void SaveReferralAsync() {
            if (string.IsNullOrEmpty(SpecialistsText) ) return;

            Referrals.Add(new Referral
            {
                PatientJMB = SelectedPatient.JMB,
                DiseaseCode = DiseaseCode,
                Date = DateTime.Now,
                Specialists = SpecialistsText,
                ExaminationId = _examination.ExaminationId, // Will be set on save
                DoctorJMB = _doctor.JMB,
            });

            CloseReferral();
            //After saving diagnosis to my collection, close AddDiagnosis window
        }
        private void AddDiagnosis()
        {
            _diagnosisView = new AddDiagnosisView(this)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            bool? result = _diagnosisView.ShowDialog();
            if (result == true)
            {
                // Clear fields after successful save and window close
                DiagnosisOpinion = string.Empty;
                SelectedDisease = null;
               
            }
        }
        private void AddReferral()
        {
            _referralView = new ReferralView(this)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            bool? result = _referralView.ShowDialog();
            if (result == true)
            {
                DiagnosisOpinion = string.Empty;
                SelectedDisease = null;
            }
        }

        private bool CanAddDiagnosis() => true;

        private void RemoveDiagnosis(Diagnosis diagnosis)
        {
            Diagnosis.Remove(diagnosis);
        }

        
        private bool CanAddReferral() => true;

        private void RemoveReferral(Referral referral)
        {
            Referrals.Remove(referral);
        }

        private void AddProcurement()
        {
            if (ProcurementQuantity <= 0) return;

            Procurements.Add(new Procurement
            {
                Quantity = ProcurementQuantity,
                ProcurementDate = DateTime.Now
            });

            ProcurementQuantity = 0;
        }

        private bool CanAddProcurement() => ProcurementQuantity > 0;

        private void RemoveProcurement(Procurement procurement)
        {
            Procurements.Remove(procurement);
        }

        private async Task SaveAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                //TODO neko obavjestenje
                if (_examination == null) return;

                _context.Examinations.Add(_examination);
                await _context.SaveChangesAsync();

                foreach (var diagnosis in Diagnosis)
                {
                    diagnosis.ExaminationId = _examination.ExaminationId;
                    _context.Diagnosis.Add(diagnosis);
                }

                foreach (var referral in Referrals)
                {
                    referral.ExaminationId = _examination.ExaminationId;
                    _context.Referrals.Add(referral);
                }

                foreach (var procurement in Procurements)
                {
                    _context.Procurements.Add(procurement);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                MessageBox.Show("Examination saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                 CloseExamination();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                MessageBox.Show($"Error saving examination: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private bool CanSave() => true;
        private void Cancel()
        {
            _view.CloseWindow();

        }
        private void CloseExamination()
        {
            _view.CloseWindow();
        }
        private void CancelDiagnosis() {
            _diagnosisView.CloseWindow();
        }
        private void CancelReferral() {
            _referralView.CloseWindow();
        }
        private void CloseReferral() {
            _referralView.CloseWindow();
        }
        private void CloseDiagnosis()
        {
            _diagnosisView.CloseWindow();
        }
    }
}