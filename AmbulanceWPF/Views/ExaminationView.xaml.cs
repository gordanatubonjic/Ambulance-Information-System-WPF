using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using AmbulanceWPF.Models;
using AmbulanceWPF.ViewModels;

namespace AmbulanceWPF.Views
{

    public partial class ExaminationView : Window
    {
        public ExaminationView(Patient patient, Employee doctor)
        {
            InitializeComponent();
            DataContext = new ExaminationViewModel(patient, doctor, this);
        }
        public ExaminationView(Employee doctor) {
            InitializeComponent();
            DataContext = new ExaminationViewModel( doctor, this);
        }
        public ExaminationView(Examination examination) {
            InitializeComponent();
            //TODO Koji arg treba proslijediti
         // DataContext = new ExaminationViewModel();
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
                Close();
            }
            base.OnKeyDown(e);
        }
    }
}
