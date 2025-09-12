using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using AmbulanceWPF.Views;
using System.Collections.ObjectModel;
using AmbulanceWPF.Models;
using AmbulanceWPF.Repository;

namespace AmbulanceWPF.ViewModels
{
    public class PatientHistoryViewModel
    {
        // Add this method to your ViewModel or wherever you initialize the data
        public PatientHistory History { set; get; }
        
        public Patient Patient { get; set; }

        public PatientHistoryViewModel() { }

        // In your ViewModel constructor or initialization method:
        public PatientHistoryViewModel(Patient P)
        {
            Patient = PatientRepository.GetPatient(P.JMB);
            History = Patient.History;
           
        }


        private ICommand _showDetailedReportCommand;
        public ICommand ShowDetailedReportCommand
        {
            get
            {
                if (_showDetailedReportCommand == null)
                {
                    _showDetailedReportCommand = new RelayCommand<CheckUp>(ShowDetailedReport);
                }
                return _showDetailedReportCommand;
            }
        }

        private void ShowDetailedReport(CheckUp checkup)
        {
            if (checkup == null) return;

            // Create and show the detailed report window
            var detailWindow = new CheckupDetailWindow
            {
                DataContext = checkup,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            detailWindow.Show(); // Use Show() for non-modal or ShowDialog() for modal
        }

        // Sample CheckUp model (adjust properties according to your needs)
        public class CheckUp : INotifyPropertyChanged
        {
            public DateTime CheckupDate { get; set; }
            public string DoctorName { get; set; }
            public string Summary { get; set; }
            public string CheckupType { get; set; } // e.g., "Routine", "Follow-up", "Emergency"
            public string DetailedDescription { get; set; }
            public string Diagnosis { get; set; }
            public string Medications { get; set; }
            public string NextAppointment { get; set; }
            public List<string> TestResults { get; set; }
            public string Notes { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
