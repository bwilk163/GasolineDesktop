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
    /// Interaction logic for View_ViewGasStation.xaml
    /// </summary>
    public partial class View_ViewGasStation : Page
    {
        GasolineService _gasolineService;
        GasolineEntities1 ge ;

        GasStation gs;
        Frame MainFrame;

        List<FuelType> fuelTypes;
        List<GasStationFuel> gasStationFuels;


        public View_ViewGasStation(Frame mainFrame, GasStation gasStation, GasolineService gasolineService)
        {
            _gasolineService = gasolineService;
            ge = new GasolineEntities1();
            MainFrame = mainFrame;
            InitializeComponent();

            ge = new GasolineEntities1();
            gs = gasStation;

            station_name.Content = gs.Name;
            station_postalcode.Content = gs.PostalCode;
            station_street.Content = gs.Street;
            station_city.Content = gs.City;


            fuelTypes = _gasolineService.GetAllFuelTypes();
            gasStationFuels = ge.GasStationFuels.Where(x => x.GasStationId == gs.Id).ToList();

            UpdateFuels();
        }

        private void RemoveGasStation_click(object sender, RoutedEventArgs e)
        {
            _gasolineService.RemoveGasStation(gs.Id);

            MainFrame.Content = new View_GasStations(MainFrame, _gasolineService);
        }


        void UpdateFuels()
        {

            for (int i = 0; i < fuelTypes.Count; i++)
            {
                var f = fuelTypes[i];

                StackPanel spInner = new StackPanel() { Orientation = Orientation.Horizontal };

                Label lbl = new Label()
                {
                    Content = f.FuelName
                };

                if (!gasStationFuels.Any(x => x.FuelTypeId == f.Id))
                {
                    Button btn = new Button()
                    {
                        Content = "Dodaj",
                        Name = "AddFuel" + i
                    };
                    btn.Click += AddFuel;
                    spInner.Children.Add(btn);
                }
                else
                {
                    TextBox price = new TextBox()
                    {
                        Name = "F" + i,
                        Text = gasStationFuels.Find(x => x.FuelTypeId == f.Id && x.GasStationId == gs.Id).Price.ToString()
                    };
                    price.KeyDown += Price_KeyDown;
                    spInner.Children.Add(price);


                }

                spInner.Children.Add(lbl);
                FuelsParent.Children.Add(spInner);
            }
        }

        private void Price_KeyDown(object sender, KeyEventArgs e)
        {
            string v = (sender as TextBox).Text.Replace(",", ".");
            if (decimal.TryParse(v, out decimal price))
            {
                if (e.Key == Key.Return)
                {
                    Guid fuelGuid = fuelTypes[int.Parse((sender as TextBox).Name.Replace("F", ""))].Id;

                    var f = ge.GasStationFuels.FirstOrDefault(x => x.FuelTypeId == fuelGuid && x.GasStationId == gs.Id);
                    
                    f.Price = price;
                    f.LastUpdateUtc = DateTime.Now;

                    ge.SaveChanges();
                    MainFrame.Content = new View_ViewGasStation(MainFrame, gs, _gasolineService);

                }
            }
        }

        void AddFuel(object sender, RoutedEventArgs e)
        {
            string v = (sender as Button).Name.Replace("AddFuel", "");
            int fuelId = int.Parse(v);

            GasStationFuel gsf = new GasStationFuel()
            {
                FuelTypeId = fuelTypes[fuelId].Id,
                GasStationId = gs.Id,
                Price = 0,
                LastUpdateUtc = DateTime.Now
            };

            ge.GasStationFuels.Add(gsf);
            ge.SaveChanges();

            MainFrame.Content = new View_ViewGasStation(MainFrame, gs, _gasolineService);
        }
    }
}