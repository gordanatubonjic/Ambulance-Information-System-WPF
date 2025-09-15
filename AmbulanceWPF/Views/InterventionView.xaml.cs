using AmbulanceWPF.Models;
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
using AmbulanceWPF.ViewModels;

namespace AmbulanceWPF.Views
{
    /// <summary>
    /// Interaction logic for InterventionView.xaml
    /// </summary>
    public partial class InterventionView : Window
    {
        private Employee _currentUser;

        public InterventionView()
        {
            InitializeComponent();
        }
        public InterventionView(Employee employee)
        {
            _currentUser = employee;
            InitializeComponent();
            this.DataContext = new InterventionViewModel(employee);

            // Set window properties for modal behavior
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
            this.ShowInTaskbar = false;
            this.Topmost = true;

            // Prevent minimizing
            this.StateChanged += InterventionView_StateChanged;
        }

        private void InterventionView_StateChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.DialogResult = false;
                this.Close();
            }
            base.OnKeyDown(e);
        }
    }
}
