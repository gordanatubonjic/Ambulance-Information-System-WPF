using AmbulanceWPF.Models;
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
    /// <summary>
    /// Interaction logic for DoctorHomePageView.xaml
    /// </summary>
    public partial class DoctorHomePageView : Window
    {
        public DoctorHomePageView()
        {
            InitializeComponent();
            DoctorHomePageViewModel homePageViewModel = new DoctorHomePageViewModel();
            this.DataContext = homePageViewModel;

        }
        public DoctorHomePageView(Employee e)
        {
            InitializeComponent();
            DoctorHomePageViewModel homePageViewModel = new DoctorHomePageViewModel(e);
            this.DataContext = homePageViewModel;

        }

        
    }
}
