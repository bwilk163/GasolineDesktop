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

namespace GasolineDesktop
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class View_AddGasStation : Page
    {
        GasolineService _gasolineService;
        Frame _mainFrame;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="mainFrame">Główna ramka</param>
        /// <param name="gasolineService">Serwis GasolineService</param>
        public View_AddGasStation(Frame mainFrame, GasolineService gasolineService)
        {
            _mainFrame = mainFrame;
            _gasolineService = gasolineService;
            InitializeComponent();
        }


        /// <summary>
        /// Wydarzenie po kliknięciu przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         void Submit_Click(object sender, RoutedEventArgs e)
        {
            GasStation gasStation = new GasStation
            {
                Id = Guid.NewGuid(),
                Name = station_name.Text,
                City = station_city.Text,
                Street = station_street.Text,
                PostalCode = station_postalcode.Text
            };

            _gasolineService.AddGasStation(gasStation);

            _mainFrame.Content = new View_GasStations(_mainFrame, _gasolineService);
        }
    }
}
