
using AmbulanceWPF.Helper;
using AmbulanceWPF.Models;
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

            public ObservableCollection<Employee> MedicalTeam { get; set; }
            public ObservableCollection<Therapy> Medications { get; set; }

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

                MedicalTeam = new ObservableCollection<Employee>();
                Medications = new ObservableCollection<Therapy>();

                SaveCommand = new RelayCommand(SaveIntervention);
                CancelCommand = new RelayCommand(Cancel);
                AddTeamMemberCommand = new RelayCommand(AddTeamMember);
                RemoveTeamMemberCommand = new RelayCommand<Employee>(RemoveTeamMember);
                AddMedicationCommand = new RelayCommand(AddMedication);
                RemoveMedicationCommand = new RelayCommand<Therapy>(RemoveMedication);

                                 MedicalTeam.Add(new Employee
                {
                    Name = $"{currentUser.Name} {currentUser.LastName}",
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
                       //logika za dodavanje intervencije
                    };

                                         
                    MessageBox.Show("Intervention saved successfully!", "Success",
                                  MessageBoxButton.OK, MessageBoxImage.Information);

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
                                 MedicalTeam.Add(new Employee { Name = "New Member", Role = "Employee" });
            }

            private void RemoveTeamMember(Employee member)
            {
                if (member != null)
                    MedicalTeam.Remove(member);
            }

            private void AddMedication()
            {
                                
            }

            private void RemoveMedication(Therapy medication)
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
                                

            }
        }
    }
    }

