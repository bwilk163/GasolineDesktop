using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace GasolineDesktop
{
    /// <summary>
    /// Interaction logic for View_FuelType.xaml
    /// </summary>
    public partial class View_FuelType : Page
    {
        GasolineService _gasolineService;
        Frame mainFrame;
        FuelType fuelType;

        List<GasStation> gasStations = new List<GasStation>();

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="_mainFrame">Ramka, w której ma zostać zmieniany widok</param>
        /// <param name="_fuelType">Instancja paliwa</param>
        /// <param name="gasolineService">Serwis GasolineService</param>
        public View_FuelType(Frame _mainFrame, FuelType _fuelType, GasolineService gasolineService)
        {
            _gasolineService = gasolineService;
            mainFrame = _mainFrame;
            fuelType = _fuelType;

            InitializeComponent();

            List<GasStationFuel> stationWithFuel = _gasolineService.GetGasStationFuelsByFuelId(fuelType.Id);

            stationWithFuel = stationWithFuel.OrderBy(x => x.Price).ToList();

            FuelName.Content = fuelType.FuelName;

            for (int i = 0; i < stationWithFuel.Count; i++)
            {
                var gf = stationWithFuel[i];
                GasStation gs = _gasolineService.GetGasStationById(gf.GasStationId);

                if (gs != null)
                {
                    // Utworzenie panelu, aby pogrupować stacje
                    StackPanel sp = new StackPanel();

                    // Utworzenie napisu z nazwą stacji i ceną
                    Label lbl = new Label() { Name = "StationName" };
                    lbl.Content = gs.Name + ", " + gf.Price + " zł";
                    sp.Children.Add(lbl);

                    // Utworzenie przycisku
                    Button btn = new Button() { Name = "B" + i, Content = "Zobacz" };
                    btn.Click += ViewStation_ButtonClick;
                    sp.Children.Add(btn);

                    // Przypisanie grupy do widoku
                    Stations.Children.Add(sp);
                }

                gasStations.Add(gs);
            }
        }

        /// <summary>
        /// Funkcja wywoływana przez przycisk
        /// </summary>
        /// <param name="sender">Źródło wywołania funkcji</param>
        /// <param name="e"></param>
        private void ViewStation_ButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            // Numer stacji na podstawie nazwy przycisku
            string v = (sender as Button).Name.Replace("B", "");
            int gasStationId = int.Parse(v);
            GasStation gs = gasStations[gasStationId];

            // Zmiana widoku
            mainFrame.Content = new View_ViewGasStation(mainFrame, gs, _gasolineService);
        }
    }
}