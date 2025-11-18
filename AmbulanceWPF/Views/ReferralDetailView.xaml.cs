using System.Windows;

namespace AmbulanceWPF.Views
{
    public partial class ReferralDetailView : Window
    {
        public ReferralDetailView()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}