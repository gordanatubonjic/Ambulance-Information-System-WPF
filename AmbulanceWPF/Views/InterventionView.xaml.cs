using AmbulanceWPF.Models;
using AmbulanceWPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceWPF.Views
{
    public partial class InterventionView : Window
    {
        public InterventionView(Employee employee)
        {
            InitializeComponent();
            DataContext = new InterventionViewModel(employee);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
            }
            base.OnKeyDown(e);
        }
    }
}