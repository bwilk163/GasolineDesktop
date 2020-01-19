using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GasolineDesktop.Tests
{
    [TestClass]
    public class UnitTests
    {

        /// <summary>
        /// Test metody dodającej rekord do bazy
        /// </summary>
        [TestMethod]
        public void AddNewGasStationTest()
        {
            GasolineService gs = new GasolineService();
            GasolineEntities1 ge = new GasolineEntities1();

            var newGs = new GasStation()
            {
                Name = "TEST"
            };

            var result = gs.AddGasStation(newGs);

            Assert.IsNotNull(result);

            // Cleanup after test
            ge.GasStations.Remove(ge.GasStations.FirstOrDefault(x => x.Name == "TEST"));
        }


        /// <summary>
        /// Test metody usuwającej rekord z bazy
        /// </summary>
        [TestMethod]
        public void RemoveGasStation()
        {
            GasolineService gservice = new GasolineService();
            GasolineEntities1 ge = new GasolineEntities1();

            var gs = ge.GasStations.Add(new GasStation() { });
            ge.SaveChanges();

            var result = gservice.RemoveGasStation(gs.Id);

            // Jeżeli result = true, znaczy to, że rekord został usunięty z bazy
            Assert.AreEqual(result, true);
        }
    }
}