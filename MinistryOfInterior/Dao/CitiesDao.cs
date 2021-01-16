using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using log4net;

namespace MinistryOfInterior.Dao
{
    public class CitiesDao
    {
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public static List<City> Get()
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    return entities.Cities.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
            
        }

        public static City GetById(long id)
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    return entities.Cities
                        .Single(city => city.ID == id);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }


        public static void Add(City city)
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    entities.Cities.AddOrUpdate(city);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }

        public static void Update(long id, City cityToUpdate)
        {
            try
            {

                using (var entities = new MinistryOfInteriorEntities())
                {
                    var city = entities.Cities
                        .Single(city1 => city1.ID == id);

                    city.Name = cityToUpdate.Name;
                    city.Mayor = cityToUpdate.Mayor;
                    city.Population = cityToUpdate.Population;

                    entities.Cities.AddOrUpdate(city);
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
                    var city = entities.Cities
                        .Single(city1 => city1.ID == id);

                    entities.Cities.Remove(city);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }

        public static List<City> GetByPopulation(long population)
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    return entities.Cities
                        .Where(city => city.Population >= population)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }

        public static List<City> GetWithQuerySyntax()
        {
            try
            {
                using (var entities = new MinistryOfInteriorEntities())
                {
                    return (from cities in entities.Cities
                        select cities).ToList();
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
