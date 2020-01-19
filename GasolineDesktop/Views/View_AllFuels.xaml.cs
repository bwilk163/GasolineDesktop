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
    /// Interaction logic for View_AllFuels.xaml
    /// </summary>
    public partial class View_AllFuels : Page
    {
        GasolineService _gasolineService;
        Frame mainFrame;
        List<FuelType> fuelTypes;

        public View_AllFuels(Frame _mainFrame, GasolineService gasolineService)
        {
            _gasolineService = gasolineService;
            mainFrame = _mainFrame;
            InitializeComponent();


            fuelTypes = _gasolineService.GetAllFuelTypes();

            for(int i = 0; i < fuelTypes.Count; i++)
            {
                Button btn = new Button() { Name = "B" + i, Content=fuelTypes[i].FuelName };
                btn.Click += Btn_Click;

                AllFuels.Children.Add(btn);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button snder = sender as Button;
            string snderName = snder.Name;
            snderName = snderName.Replace("B", "");

            mainFrame.Content = new View_FuelType(mainFrame, fuelTypes[int.Parse(snderName)],_gasolineService);
        }
    }
}
