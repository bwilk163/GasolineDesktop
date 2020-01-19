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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GasolineService _gasolineService;
        public MainWindow()
        {
            InitializeComponent();

            _gasolineService = new GasolineService();
        }
        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new View_AddGasStation(Main, _gasolineService);
        }

        private void ViewStationsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new View_GasStations(Main, _gasolineService);
        }

        private void ViewFuelsTypesButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new View_AllFuels(Main, _gasolineService);
        }
    }
}
