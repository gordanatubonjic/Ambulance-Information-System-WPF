// AddExaminationWindow.xaml.cs
using AmbulanceWPF.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Data;

namespace AmbulanceWPF.Views
{
    public partial class AddExaminationView : Window, IDataErrorInfo
    {
        private readonly Examination _examination;
        public bool IsBusy { get; private set; }
        public bool IsValid => string.IsNullOrEmpty(Error);

        public AddExaminationView(Examination examination)
        {
            _examination = examination ?? throw new ArgumentNullException(nameof(examination));
            DataContext = _examination;
            InitializeComponent();
            // For TestResults, bind manually since it's a list
        }

        public string Error => ValidateAll();

        public string this[string columnName]
        {
            get
            {
                // Use DataAnnotations for validation
                var validationContext = new ValidationContext(_examination) { MemberName = columnName };
                var results = new System.Collections.Generic.List<ValidationResult>();
                if (!Validator.TryValidateProperty(_examination.GetType().GetProperty(columnName)?.GetValue(_examination), validationContext, results))
                {
                    return results[0].ErrorMessage;
                }
                return null;
            }
        }

        private string ValidateAll()
        {
            // Check all properties
            foreach (var prop in typeof(Examination).GetProperties())
            {
                var err = this[prop.Name];
                if (!string.IsNullOrEmpty(err)) return err;
            }
            return null;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Error))
            {
                MessageBox.Show("Please fix validation errors.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                IsBusy = true;
                // Parse TestResults
                //_examination.TestResults = TestResultsTextBox.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}