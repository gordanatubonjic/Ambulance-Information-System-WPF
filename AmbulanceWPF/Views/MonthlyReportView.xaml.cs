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
    
        public partial class MonthlyReportView : Window
        {
            public MonthlyReportView()
            {
                InitializeComponent();
            }

            private void OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (DataContext is MonthlyReportViewModel vm)
                {
                    vm.LoadReportsCommand.Execute(null);
                }
            }
        }
    
}
