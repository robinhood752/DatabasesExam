using System.Collections.Generic;
using MinistryOfInterior.Dao;

namespace MinistryOfInterior
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<City> cities = CitiesDao.Get();
            List<District> districts = DistrictsDao.Get();

            DistrictsDao.UpdatePopulation();
        }
    }
}
