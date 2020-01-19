using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GasolineDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GasolineEntities1 databaseContext = new GasolineEntities1();

            if(!databaseContext.GasStations.Any())
            {
                databaseContext.GasStations.Add(new GasStation()
                {
                    Name = "Lotos",
                    Id = Guid.NewGuid(),
                    City = "Rzeszów",
                    Street = "Hetmanska 120",
                    PostalCode = "35-001"
                });
            }

        }
    }
}
