using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasolineDesktop
{
    public class GasolineService
    {
        GasolineEntities1 gasolineEntities;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public GasolineService()
        {
            gasolineEntities = new GasolineEntities1();
        }


        /// <summary>
        /// Pobranie wszystkich rekordów z tabeli FuelTypes
        /// </summary>
        /// <returns>Lista wszystkich FuelTypes</returns>
        public List<FuelType> GetAllFuelTypes()
        {
            return gasolineEntities.FuelTypes.ToList();
        }


        /// <summary>
        /// Dodanie nowej stacji do bazy
        /// </summary>
        /// <param name="gasStation">Instancja klasy GasStation</param>
        /// <returns>Utworzony rekord</returns>
        public GasStation AddGasStation(GasStation gasStation)
        {
            var x = gasolineEntities.GasStations.Add(gasStation);

            gasolineEntities.SaveChanges();

            return x;
        }


        /// <summary>
        /// Pobranie wszystkich rekordów z tabeli GasStations
        /// </summary>
        /// <returns>Lista wszystkich GasStation</returns>
        public List<GasStation> GetAllGasStations()
        {
            return gasolineEntities.GasStations.ToList();
        }

        /// <summary>
        /// Wyszukanie stacji paliw po jej Guid
        /// </summary>
        /// <param name="guid">Guid szukanego elementu</param>
        /// <returns>Stacja paliw</returns>
        public GasStation GetGasStationById(Guid guid)
        {
            return gasolineEntities.GasStations.FirstOrDefault(x => x.Id == guid);
        }

        /// <summary>
        /// Usunięcie stacji z bazy
        /// </summary>
        /// <param name="guid">Guid usuwanej stacji</param>
        /// <returns>true jeżeli znaleziono i usunięto, false jeśli nie</returns>
        public bool RemoveGasStation(Guid guid)
        {
            GasStation gs = gasolineEntities.GasStations.FirstOrDefault(x => x.Id == guid);

            if (gs != null)
            {
                var remove = gasolineEntities.GasStations.Remove(gs);

                gasolineEntities.SaveChanges();

                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Wyszukanie wszyskich stacji na których znajduje się dane paliwo
        /// </summary>
        /// <param name="fuelId">Guid szukanego paliwa</param>
        /// <returns>Lista stacji paliw z szukanym typem paliwa</returns>
        public List<GasStationFuel> GetGasStationFuelsByFuelId(Guid fuelId)
        {
            return gasolineEntities.GasStationFuels.Where(x => x.FuelTypeId == fuelId).ToList();
        }
    }
}