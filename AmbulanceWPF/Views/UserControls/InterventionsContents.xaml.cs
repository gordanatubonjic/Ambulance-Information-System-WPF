using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using AmbulanceWPF.Models;
using AmbulanceWPF.ViewModels;

namespace AmbulanceWPF.Views.UserControls
{
    public partial class InterventionsContents : UserControl
    {
        private ObservableCollection<Intervention> _interventions;

        public InterventionsContents(ObservableCollection<Intervention> interventions)
        {
            InitializeComponent();
            _interventions = interventions;
            DataContext = this; // Or set to a specific ViewModel
            LoadInterventionsData();
        }

        private void LoadInterventionsData()
        {
            // Bind your data to UI controls
            InterventionsListView.ItemsSource = _interventions;
        }

    }
}
