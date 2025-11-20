// Modified PatientHistoryView.xaml.cs (minimal changes)
using AmbulanceWPF.Models;
using AmbulanceWPF.ViewModels;
using System.Windows;

namespace AmbulanceWPF.Views
{
    public partial class PatientHistoryView : Window
    {
        
        public PatientHistoryView(Patient patient, Employee user)
        {
            InitializeComponent();
            // For design-time or default
            DataContext = new PatientHistoryViewModel(patient, user);
        }
        public void CloseWindow()
        {
            this.DialogResult = true; // or false
            this.Close();
        }
        public PatientHistoryView(PatientHistoryViewModel phvm)
        {
            InitializeComponent();
            DataContext = phvm;
        }
    }
}