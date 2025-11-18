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

namespace AmbulanceWPF.ViewModels
{
    public class ExaminationViewModel : ViewModelBase
    {
        private readonly AmbulanceDbContext _context;
        private readonly Patient _patient;
        private readonly Employee _doctor;
        private Examination _examination;
        private string _diagnosisOpinion;
        private DiseaseCatalog _selectedDisease;
        private string _referralSpecialists;
        private DiseaseCatalog _selectedReferralDisease;
        private decimal _procurementQuantity;

        public ObservableCollection<DiseaseCatalog> Diseases { get; } = new();
        public ObservableCollection<Diagnosis> Diagnosis { get; } = new();
        public ObservableCollection<Referral> Referrals { get; } = new();
        public ObservableCollection<Procurement> Procurements { get; } = new();

        public Examination Examination
        {
            get => _examination;
            set { _examination = value; OnPropertyChanged(nameof(Examination)); }
        }

        public Patient Patient => _patient;
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

        public ExaminationViewModel(Patient patient, Employee doctor)
        {
            _patient = patient;
            _doctor = doctor;
            _context = new AmbulanceDbContext();
            _examination = new Examination
            {
                PatientJMB = patient.JMB,
                DoctorJMB = doctor.JMB,
                ExaminationDate = DateTime.Now
            };

            LoadDiseasesAsync();

            AddDiagnosisCommand = new RelayCommand(AddDiagnosis, CanAddDiagnosis);
            RemoveDiagnosisCommand = new RelayCommand<Diagnosis>(RemoveDiagnosis);
            AddReferralCommand = new RelayCommand(AddReferral, CanAddReferral);
            RemoveReferralCommand = new RelayCommand<Referral>(RemoveReferral);
            AddProcurementCommand = new RelayCommand(AddProcurement, CanAddProcurement);
            RemoveProcurementCommand = new RelayCommand<Procurement>(RemoveProcurement);
            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            CancelCommand = new RelayCommand(Cancel);
        }
        public ExaminationViewModel(Employee doctor)
        {
            
            _doctor = doctor;
            _context = new AmbulanceDbContext();
            /*_examination = new Examination
            {
               
                DoctorJMB = doctor.JMB,
                ExaminationDate = DateTime.Now
            };*/
            //TODO Moram settovati pacijenta iz prozora

            LoadDiseasesAsync();

            AddDiagnosisCommand = new RelayCommand(AddDiagnosis, CanAddDiagnosis);
            RemoveDiagnosisCommand = new RelayCommand<Diagnosis>(RemoveDiagnosis);
            AddReferralCommand = new RelayCommand(AddReferral, CanAddReferral);
            RemoveReferralCommand = new RelayCommand<Referral>(RemoveReferral);
            AddProcurementCommand = new RelayCommand(AddProcurement, CanAddProcurement);
            RemoveProcurementCommand = new RelayCommand<Procurement>(RemoveProcurement);
            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            CancelCommand = new RelayCommand(Cancel);
        }

        private async Task LoadDiseasesAsync()
        {
            var diseases = await _context.DiseaseCatalogs.ToListAsync();
            foreach (var disease in diseases)
            {
                Diseases.Add(disease);
            }
        }

        private void AddDiagnosis()
        {
            if (SelectedDisease == null || string.IsNullOrEmpty(DiagnosisOpinion)) return;

            Diagnosis.Add(new Diagnosis
            {
                PatientJMB = _patient.JMB,
                DiseaseCode = SelectedDisease.DiseaseCode,
                Date = DateTime.Now,
                DoctorOpinion = DiagnosisOpinion,
                ExaminationId = _examination.ExaminationId, // Will be set on save
                DoctorJMB = _doctor.JMB
            });

            DiagnosisOpinion = string.Empty;
            SelectedDisease = null;
        }

        private bool CanAddDiagnosis() => SelectedDisease != null && !string.IsNullOrEmpty(DiagnosisOpinion);

        private void RemoveDiagnosis(Diagnosis diagnosis)
        {
            Diagnosis.Remove(diagnosis);
        }

        private void AddReferral()
        {
            if (SelectedReferralDisease == null || string.IsNullOrEmpty(ReferralSpecialists)) return;

            Referrals.Add(new Referral
            {
                DiseaseCode = SelectedReferralDisease.DiseaseCode,
                Specialists = ReferralSpecialists,
                DoctorJMB = _doctor.JMB,
                Date = DateTime.Now,
                ExaminationId = _examination.ExaminationId // Will be set on save
            });

            ReferralSpecialists = string.Empty;
            SelectedReferralDisease = null;
        }

        private bool CanAddReferral() => SelectedReferralDisease != null && !string.IsNullOrEmpty(ReferralSpecialists);

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
                Window.GetWindow((DependencyObject)(this as object))?.Close();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                MessageBox.Show($"Error saving examination: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private bool CanSave() => !string.IsNullOrEmpty(_examination.ExaminationDescription) && Diagnosis.Any();
        private bool CanSave() => true;
        private void Cancel()
        {
            //Window.GetWindow(this as object)?.Close();
        }

        }
}