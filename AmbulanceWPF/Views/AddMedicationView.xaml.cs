using AmbulanceWPF.Data;
using AmbulanceWPF.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceWPF.Views
{
    public partial class AddMedicationView : Window
    {
        public MedicationCatalog SelectedMedication { get; private set; }
        public string Dosage { get; private set; }

        public AddMedicationView()
        {
            InitializeComponent();
            LoadMedications();

            AddButton.Click += AddButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void LoadMedications()
        {
            using var context = new AmbulanceDbContext();
            MedicationComboBox.ItemsSource = context.MedicationCatalogs
                .Where(m => m.IsActive)
                .OrderBy(m => m.Name)
                .ToList();
            MedicationComboBox.DisplayMemberPath = "Name";
            MedicationComboBox.SelectedValuePath = "MedicationCode";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedMedication = MedicationComboBox.SelectedItem as MedicationCatalog;
            Dosage = DosageTextBox.Text.Trim();

            if (SelectedMedication == null || string.IsNullOrEmpty(Dosage))
            {
                MessageBox.Show("Please select a medication and enter dosage.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddButton_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                CancelButton_Click(null, null);
            }
            base.OnKeyDown(e);
        }
    }
}