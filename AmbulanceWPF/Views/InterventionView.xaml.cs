using AmbulanceWPF.Models;
using AmbulanceWPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceWPF.Views
{
    public partial class InterventionView : Window
    {
        
        public InterventionView(Employee currentUser)
        {
            InitializeComponent();
            DataContext = new InterventionViewModel(currentUser, this); // Pass 'this' to VM
        }

        public void CloseWindow()
        {
            this.DialogResult = true; // or false
            this.Close();
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