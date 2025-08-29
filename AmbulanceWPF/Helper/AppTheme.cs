using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceWPF.Helper
{
    class AppTheme
    {


        public static void ChangeTheme(Uri themeURI) {
            ResourceDictionary Theme = new ResourceDictionary()
            {
                Source = themeURI
            };
            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(Theme);
           
        
        }
    }
}
