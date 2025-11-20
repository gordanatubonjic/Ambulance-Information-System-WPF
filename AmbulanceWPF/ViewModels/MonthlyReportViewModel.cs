using AmbulanceWPF.Data;
using AmbulanceWPF.Models;
using AmbulanceWPF.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AmbulanceWPF.ViewModels
{
    public class MonthlyReportViewModel : INotifyPropertyChanged
    {
        private DateTime? _selectedMonth;
        private ObservableCollection<MonthlyReport> _reports;
        private bool _isLoading;

        public DateTime? SelectedMonth
        {
            get => _selectedMonth;
            set => SetProperty(ref _selectedMonth, value);
        }

        public ObservableCollection<MonthlyReport> Reports
        {
            get => _reports;
            set => SetProperty(ref _reports, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand LoadReportsCommand { get; }
        public ICommand CloseReportCommand { get; }

        public MonthlyReportViewModel()
        {
            Reports = new ObservableCollection<MonthlyReport>();
            LoadReportsCommand = new AsyncRelayCommand(LoadReportsAsync);
            CloseReportCommand = new RelayCommand(CloseReport);
        }
        private async void OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await LoadReportsAsync();
        }
        private void CloseReport()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MonthlyReportView)
                {
                    window.Close();
                    break;
                }
            }
        } 
        private async Task LoadReportsAsync()
        {
            if (!SelectedMonth.HasValue) return;

            IsLoading = true;
            Reports.Clear();

            try
            {
                using (var context = new AmbulanceDbContext())
                {
                    var startDate = new DateTime(SelectedMonth.Value.Year, SelectedMonth.Value.Month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    // Get all employees first
                    var employees = await context.Employees.ToListAsync();

                    foreach (var employee in employees)
                    {
                        var patientCount = await context.MedicalRecords
                            .Where(mr => mr.DoctorJMB == employee.JMB)
                            .Select(mr => mr.PatientJMB)
                            .Distinct()
                            .CountAsync();

                        var examinationCount = await context.Examinations
                            .Where(e => e.DoctorJMB == employee.JMB &&
                                       e.ExaminationDate >= startDate && e.ExaminationDate <= endDate)
                            .CountAsync();

                        var interventionCount = await context.InterventionDoctors
                            .Where(id => id.DoctorJMB == employee.JMB &&
                                        id.Intervention.Date >= startDate &&
                                        id.Intervention.Date <= endDate)
                            .CountAsync();

                        if (patientCount > 0 || examinationCount > 0 || interventionCount > 0)
                        {
                            Reports.Add(new MonthlyReport
                            {
                                Employee = employee.FullName,
                                NumberOfPatients = patientCount,
                                NumberOfExaminations = examinationCount,
                                NumberOfInterventions = interventionCount
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reports: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
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