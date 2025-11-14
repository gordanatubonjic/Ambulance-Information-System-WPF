using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AmbulanceWPF.Views
{
          public partial class AddMedicationView : Window
    {
        
        public string SelectedMedication { get; private set; }
        public string Dosage { get; private set; }

        public AddMedicationView()
        {
            InitializeComponent();

                         AddButton.Click += AddButton_Click;
            CancelButton.Click += CancelButton_Click;

                         this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
            this.ShowInTaskbar = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
                         SelectedMedication = MedicationComboBox.SelectedItem?.ToString();
            Dosage = DosageTextBox.Text;

            if (string.IsNullOrEmpty(SelectedMedication) || string.IsNullOrEmpty(Dosage))
            {
                MessageBox.Show("Please select a medication and enter dosage.", "Validation Error",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
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
