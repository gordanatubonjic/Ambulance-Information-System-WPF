using System.Windows;

namespace AmbulanceWPF.Views
{
    public partial class ExaminationDetailView : Window
    {
        public ExaminationDetailView()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}