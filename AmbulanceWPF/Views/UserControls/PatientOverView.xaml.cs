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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AmbulanceWPF.ViewModels;

namespace AmbulanceWPF.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PatientOverView.xaml
    /// </summary>
    public partial class PatientOverView : UserControl
    {
        public PatientOverView()
        {
            InitializeComponent();
            // Remove any DataContext setting from here
            // Let the parent control set the DataContext
        }
        
    }
}
