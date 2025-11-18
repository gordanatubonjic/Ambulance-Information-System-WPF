using System.Windows;

namespace AmbulanceWPF.Views
{
    public partial class DiagnosisDetailView : Window
    {
        public DiagnosisDetailView()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}