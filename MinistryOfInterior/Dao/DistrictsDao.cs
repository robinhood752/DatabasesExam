using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using log4net;

namespace MinistryOfInterior.Dao
{
    class DistrictsDao
    {
        private static readonly ILog Logger =
               LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public static List<District> Get()
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    return entities.Districts.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }

        }

        public static District GetById(long id)
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    return entities.Districts
                        .Single(district => district.ID == id);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }

        public static void Add(District district)
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    entities.Districts.AddOrUpdate(district);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }

        public static void Update(long id, District districtToUpdate)
        {
            try
            {

                using (var entities = new MinistryOfInteriorEntities())
                {
                    var district = entities.Districts
                        .Single(district1 => district1.ID == id);

                    district.Name = districtToUpdate.Name;
                    district.Population = districtToUpdate.Population;

                    entities.Districts.AddOrUpdate(district);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }

        public static void Delete(long id)
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    var district = entities.Districts
                        .Single(district1 => district1.ID == id);

                    entities.Districts.Remove(district);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }

        public static void UpdatePopulation()
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    var districts = entities.Cities
                        .GroupBy(city => city.District_ID)
                        .Select(city => new
                        {
                            id = city.FirstOrDefault().District_ID,
                            population = city.Sum(c => c.Population),
                        }).ToList();

                    districts.ForEach(district =>
                    {
                        entities.Districts
                            .Single(district1 => district1.ID == district.id)
                            .Population = district.population;
                    });

                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }
    }
}
