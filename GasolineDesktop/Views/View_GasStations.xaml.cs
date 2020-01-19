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
    /// Interaction logic for View_GasStations.xaml
    /// </summary>
    public partial class View_GasStations : Page
    {
        GasolineService _gasolineService;

        /// <summary>
        /// Zbiór wszystkich stacji paliw
        /// </summary>
        List<GasStation> gasStations;

        /// <summary>
        /// Ramka aplikacji, która pozwala na zmianę aktualnego widoku
        /// </summary>
        Frame MainFrame;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="mainFrame">Referencja do głównej ramki w aplikacji w celu umożliwienia zmiany widoku/strony</param>
        /// <param name="gasolineService">Przekazanie serwisu GasolineService</param>
        public View_GasStations(Frame mainFrame, GasolineService gasolineService)
        {
            MainFrame = mainFrame;
            _gasolineService = gasolineService;

            InitializeComponent();

            gasStations = _gasolineService.GetAllGasStations();

            GasStationParent.Children.Clear();


            // Utworzenie nowych przycisków
            for (int i = 0; i < gasStations.Count; i++)
            {
                GasStation gs = gasStations[i];

                // Utworzenie nowego napisu, wraz z nadaniem mu wartości
                System.Windows.Controls.Label newLbl = new Label
                {
                    Content = $"{gs.Name}, {gs.City}, {gs.Street}",
                    Name = $"Label{i}"
                };

                // Utworzenie nowego przycisku, wraz z nadaniem mu wartości
                System.Windows.Controls.Button newBtn = new Button()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(newLbl.ActualWidth)
                };
                newBtn.Content = "Wyświetl";
                newBtn.Name = $"B{i}";

                // Przypisanie handlera do przycisku
                newBtn.Click += ViewStationHandler;

                // Dodanie przycisku, oraz napisu do widoku
                GasStationParent.Children.Add(newLbl);
                GasStationParent.Children.Add(newBtn);
            }
        }


        /// <summary>
        /// Zdarzenie wywołane przez przycisk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ViewStationHandler(object sender, RoutedEventArgs e)
        {

            // Odczytanie numeru stacji paliw 
            Button snder = sender as Button;
            string snderName = snder.Name;
            snderName = snderName.Replace("B", "");

            // Zmiana aktualnego widoku
            MainFrame.Content = new View_ViewGasStation(MainFrame, gasStations[int.Parse(snderName)], _gasolineService);
        }
    }
}