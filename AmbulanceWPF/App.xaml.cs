using AmbulanceWPF.Database;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AmbulanceWPF
{
          public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using var context = new Data.AmbulanceDbContext();
            context.Database.EnsureCreated();

                        
        }
    }

    }
