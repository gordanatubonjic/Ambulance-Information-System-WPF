using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceWPF.Helper
{
    
        public static class WindowExtensions
        {
            public static void CloseWithResult(this Window window, bool dialogResult)
            {
                window.DialogResult = dialogResult;
                window.Close();
            }
        }
}
