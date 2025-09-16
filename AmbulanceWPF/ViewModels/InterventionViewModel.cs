
using AmbulanceWPF.Helper;
using AmbulanceWPF.Models;
    using AmbulanceWPF.Repository;
    using AmbulanceWPF.Views;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    namespace AmbulanceWPF.ViewModels
    {
        public class InterventionViewModel : INotifyPropertyChanged
        {
            private Employee _currentUser;
            private string _patientName;
            private string _patientSurname;
            private string _description;
            private string _allergies;
            private string _procedures;

            public ObservableCollection<MedicalTeamMember> MedicalTeam { get; set; }
            public ObservableCollection<Medication> Medications { get; set; }

            public ICommand SaveCommand { get; }
            public ICommand CancelCommand { get; }
            public ICommand AddTeamMemberCommand { get; }
            public ICommand RemoveTeamMemberCommand { get; }
            public ICommand AddMedicationCommand { get; }
            public ICommand RemoveMedicationCommand { get; }

            public string PatientName
            {
                get => _patientName;
                set { _patientName = value; OnPropertyChanged(nameof(PatientName)); }
            }

            public string PatientSurname
            {
                get => _patientSurname;
                set { _patientSurname = value; OnPropertyChanged(nameof(PatientSurname)); }
            }

            public string Description
            {
                get => _description;
                set { _description = value; OnPropertyChanged(nameof(Description)); }
            }

            public string Allergies
            {
                get => _allergies;
                set { _allergies = value; OnPropertyChanged(nameof(Allergies)); }
            }

            public string Procedures
            {
                get => _procedures;
                set { _procedures = value; OnPropertyChanged(nameof(Procedures)); }
            }

            public InterventionViewModel(Employee currentUser)
            {
                _currentUser = currentUser;

                MedicalTeam = new ObservableCollection<MedicalTeamMember>();
                Medications = new ObservableCollection<Medication>();

                SaveCommand = new RelayCommand(SaveIntervention);
                CancelCommand = new RelayCommand(Cancel);
                AddTeamMemberCommand = new RelayCommand(AddTeamMember);
                RemoveTeamMemberCommand = new RelayCommand<MedicalTeamMember>(RemoveTeamMember);
                AddMedicationCommand = new RelayCommand(AddMedication);
                RemoveMedicationCommand = new RelayCommand<Medication>(RemoveMedication);

                // Auto-add the current user as the responsible doctor
                MedicalTeam.Add(new MedicalTeamMember
                {
                    Name = $"{currentUser.Name} {currentUser.Surname}",
                    Role = currentUser.Role
                });
            }

            private void SaveIntervention()
            {
                if (string.IsNullOrEmpty(PatientName) || string.IsNullOrEmpty(PatientSurname))
                {
                    MessageBox.Show("Patient name and surname are required.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    var intervention = new Intervention
                    {
                        Patient = $"{PatientName} {PatientSurname}",
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Description = Description,
                        Doctor = _currentUser.Username
                    };

                    // Save to repository
                    InterventionRepository.AddIntervention(intervention);

                    MessageBox.Show("Intervention saved successfully!", "Success",
                                  MessageBoxButton.OK, MessageBoxImage.Information);

                    // Close the window with DialogResult = true
                    Application.Current.Windows.OfType<InterventionView>().FirstOrDefault()?.CloseWithResult(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving intervention: {ex.Message}", "Error",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void Cancel()
            {
                Application.Current.Windows.OfType<InterventionView>().FirstOrDefault()?.CloseWithResult(false);
            }

            private void AddTeamMember()
            {
                // Implementation for adding team members
                MedicalTeam.Add(new MedicalTeamMember { Name = "New Member", Role = "Doctor" });
            }

            private void RemoveTeamMember(MedicalTeamMember member)
            {
                if (member != null)
                    MedicalTeam.Remove(member);
            }

            private void AddMedication()
            {
                // Implementation for adding medications
                Medications.Add(new Medication { Name = "New Medication", Dosage = "0mg" });
            }

            private void RemoveMedication(Medication medication)
            {
                if (medication != null)
                    Medications.Remove(medication);
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        private void OpenAddMedication()
        {
            var addMedicationView = new AddMedicationView();
            addMedicationView.Owner = Application.Current.Windows.OfType<InterventionView>().FirstOrDefault();
            addMedicationView.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            bool? result = addMedicationView.ShowDialog();

            if (result == true)
            {
                // Add the medication to the list
                Medications.Add(new Medication
                {
                    Name = addMedicationView.SelectedMedication,
                    Dosage = addMedicationView.Dosage
                });
            }
        }
    }

        public class MedicalTeamMember
        {
            public string Name { get; set; }
            public string Role { get; set; }
        }

        public class Medication
        {
            public string Name { get; set; }
            public string Dosage { get; set; }
        }
    }

