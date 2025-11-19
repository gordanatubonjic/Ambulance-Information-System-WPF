using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AmbulanceWPF.Models
{
    public class MonthlyReport : INotifyPropertyChanged
    {
        private string _employee;
        private int _numberOfPatients;
        private int _numberOfExaminations;
        private int _numberOfInterventions;

        public string Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }

        public int NumberOfPatients
        {
            get => _numberOfPatients;
            set => SetProperty(ref _numberOfPatients, value);
        }

        public int NumberOfExaminations
        {
            get => _numberOfExaminations;
            set => SetProperty(ref _numberOfExaminations, value);
        }

        public int NumberOfInterventions
        {
            get => _numberOfInterventions;
            set => SetProperty(ref _numberOfInterventions, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(storage, value))
            {
                storage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}