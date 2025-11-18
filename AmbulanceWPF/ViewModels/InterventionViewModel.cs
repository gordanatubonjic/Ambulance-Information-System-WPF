using AmbulanceWPF.Data;
using AmbulanceWPF.Models;
using AmbulanceWPF.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceWPF.ViewModels
{
    public class InterventionViewModel : INotifyPropertyChanged
    {
        private readonly AmbulanceDbContext _context;
        private readonly Employee _currentDoctor;
        private Patient _selectedPatient;
        private string _interventionDescription;
        private string _proceduresDescription;
        private string _patientFirstName;
        private string _patientLastName;
        private string _patientJMB;
        private string _allergies;
        private Employee _selectedEmployee;
        private string _selectedRole;

        public ObservableCollection<InterventionDoctor> TeamMembers { get; } = new();
        public ObservableCollection<Therapy> AdministeredMedications { get; } = new();
        public ObservableCollection<Employee> AvailableEmployees { get; } = new();
        public ObservableCollection<string> Roles { get; } = new() { "Primary Doctor", "Assistant", "Consultant", "Anesthesiologist" };

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
                    //Allergies = value.Allergies;
                }
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public string InterventionDescription
        {
            get => _interventionDescription;
            set { _interventionDescription = value; OnPropertyChanged(); }
        }

        public string ProceduresDescription
        {
            get => _proceduresDescription;
            set { _proceduresDescription = value; OnPropertyChanged(); }
        }

        public string PatientFirstName
        {
            get => _patientFirstName;
            set { _patientFirstName = value; OnPropertyChanged(); }
        }

        public string PatientLastName
        {
            get => _patientLastName;
            set { _patientLastName = value; OnPropertyChanged(); }
        }

        public string PatientJMB
        {
            get => _patientJMB;
            set { _patientJMB = value; OnPropertyChanged(); }
        }
        public string PatientLocation
        {
            get => _patientJMB;
            set { _patientJMB = value; OnPropertyChanged(); }
        }

        public string Allergies
        {
            get => _allergies;
            set { _allergies = value; OnPropertyChanged(); }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set { _selectedEmployee = value; OnPropertyChanged(); }
        }

        public string SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        public ICommand FindOrCreatePatientCommand { get; }
        public ICommand AddTeamMemberCommand { get; }
        public ICommand RemoveTeamMemberCommand { get; }
        public ICommand AddMedicationCommand { get; }
        public ICommand RemoveMedicationCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public InterventionViewModel(Employee currentDoctor)
        {
            _currentDoctor = currentDoctor ?? throw new ArgumentNullException(nameof(currentDoctor));
            _context = new AmbulanceDbContext();

            LoadAvailableEmployees();

            FindOrCreatePatientCommand = new AsyncRelayCommand(FindOrCreatePatientAsync);
            AddTeamMemberCommand = new RelayCommand(AddTeamMember, CanAddTeamMember);
            RemoveTeamMemberCommand = new RelayCommand<InterventionDoctor>(RemoveTeamMember);
            AddMedicationCommand = new RelayCommand(AddMedication);
            RemoveMedicationCommand = new RelayCommand<Therapy>(RemoveMedication);
            SaveCommand = new AsyncRelayCommand(SaveInterventionAsync, CanSave);
            CancelCommand = new RelayCommand(Cancel);

            // Auto-add current doctor
            TeamMembers.Add(new InterventionDoctor
            {
                DoctorJMB = _currentDoctor.JMB,
                Role = "Primary Doctor",
                Employee = _currentDoctor
            });
        }

        private void LoadAvailableEmployees()
        {
            using var context = new AmbulanceDbContext();
            var employees = context.Employees
                .Where(e => e.Role == "Doctor" || e.Role == "MedicalTechnician")
                .OrderBy(e => e.Name)
                .ToList();
            foreach (var emp in employees)
            {
                AvailableEmployees.Add(emp);
            }
        }

        private async Task FindOrCreatePatientAsync()
        {
            Patient patient = null;

            using var context = new AmbulanceDbContext();

            if (!string.IsNullOrEmpty(PatientJMB))
            {
                patient = await context.Patients.FirstOrDefaultAsync(p => p.JMB == PatientJMB);
            }

            if (patient == null && !string.IsNullOrEmpty(PatientFirstName) && !string.IsNullOrEmpty(PatientLastName))
            {
                patient = await context.Patients.FirstOrDefaultAsync(p => p.Name == PatientFirstName && p.LastName == PatientLastName);
            }



            if (patient != null)
            {
                SelectedPatient = patient;
                MessageBox.Show("Patient found.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //TODO Kreirnje novog pacijentan koji je prosao kroz intervenciju
            // Create new patient
            string newJMB = string.IsNullOrEmpty(PatientJMB) ? GenerateUniqueJMB(context) : PatientJMB;
            Location patientLocation = await context.Locations.FirstOrDefaultAsync(p => p.Name == PatientLocation);
            //TODO Wait da su sva polja inicijalizovana
            patient = new Patient
            {
                JMB = newJMB,
                Name = PatientFirstName,
                LastName = PatientLastName,
                Allergies = Allergies,
                ResidenceLocation = patientLocation
                // Add other required fields if needed
            };

           
            context.Patients.Add(patient);
            await context.SaveChangesAsync();
            

            SelectedPatient = patient;
            MessageBox.Show("New patient created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void AddTeamMember()
        {
            if (SelectedEmployee == null || string.IsNullOrEmpty(SelectedRole))
                return;

            if (TeamMembers.Any(tm => tm.DoctorJMB == SelectedEmployee.JMB))
                return;

            TeamMembers.Add(new InterventionDoctor
            {
                DoctorJMB = SelectedEmployee.JMB,
                Role = SelectedRole,
                Employee = SelectedEmployee
            });

            SelectedEmployee = null;
            SelectedRole = null;
        }

        private bool CanAddTeamMember() => TeamMembers.Count < 10;

        private void RemoveTeamMember(InterventionDoctor member)
        {
            if (member?.DoctorJMB == _currentDoctor.JMB) return;
            TeamMembers.Remove(member);
        }

        private void AddMedication()
        {
            var addMed = new AddMedicationView();
            if (addMed.ShowDialog() != true) return;

            if (!decimal.TryParse(addMed.Dosage, out var dosage) || dosage <= 0)
            {
                MessageBox.Show("Invalid dosage.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AdministeredMedications.Add(new Therapy
            {
                MedicationCode = addMed.SelectedMedication.MedicationCode,
                Dosage = dosage,
                Medication = addMed.SelectedMedication
            });
        }

        private void RemoveMedication(Therapy therapy)
        {
            AdministeredMedications.Remove(therapy);
        }

        private async Task SaveInterventionAsync()
        {
            try
            {
                using var context = new AmbulanceDbContext();

                var intervention = new Intervention
                {
                    PatientJMB = SelectedPatient.JMB,
                    Date = DateTime.Now,
                    InterventionDescription = InterventionDescription + "\n\nProcedures: " + ProceduresDescription
                };

                context.Interventions.Add(intervention);
                await context.SaveChangesAsync(); // Auto-generates InterventionId

                foreach (var member in TeamMembers)
                {
                    context.InterventionDoctors.Add(new InterventionDoctor
                    {
                        InterventionId = intervention.InterventionId,
                        DoctorJMB = member.DoctorJMB,
                        Role = member.Role
                    });
                }

                foreach (var therapy in AdministeredMedications)
                {
                    context.Therapies.Add(new Therapy
                    {
                        InterventionId = intervention.InterventionId,
                        MedicationCode = therapy.MedicationCode,
                        Dosage = therapy.Dosage
                    });
                }

                await context.SaveChangesAsync();

                MessageBox.Show($"Intervention saved! ID: {intervention.InterventionId}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
               //TODO Da li je ovo ispravan pristup
                Window.GetWindow((DependencyObject)(this as object))?.Close(); // Close view
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving intervention: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSave()
        {
            return true  //SelectedPatient != null &&
                   //!string.IsNullOrWhiteSpace(InterventionDescription)
                   //&& TeamMembers.Any()
                   ;
        }

        private void Cancel()
        {
           // Window.GetWindow(this as object)?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}