using AmbulanceWPF.ViewModels;
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
    /     /     /     public partial class PatientHistoryView : Window
    {
        public PatientHistoryView()
        {
            InitializeComponent();
            PatientHistoryViewModel patientHistoryViewModel = new PatientHistoryViewModel();
            this.DataContext = patientHistoryViewModel;
        }
        public PatientHistoryView(PatientHistoryViewModel PHVM)
        {
            InitializeComponent();
            PatientHistoryViewModel patientHistoryViewModel = PHVM;
            this.DataContext = patientHistoryViewModel;
        }

    }
}
