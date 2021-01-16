using System;
using System.Collections.Generic;
using System.Data.SQLite;
using log4net;

namespace DepartmentMotorVehicles
{
    public class CarsDao
    {
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public Car Get(int id)
        {
            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();
            using var query = new SQLiteCommand($"select id, manufacturer, model, year from Cars where id = {id}", conn);
            using var reader = query.ExecuteReader();

            if(reader.Read())
            {
                return new Car()
                {
                    Id = (int) reader["id"],
                    Manufacturer = reader["manufacturer"].ToString(),
                    Model = reader["model"].ToString(),
                    Year = (int) reader["year"]
                };
            }

            Logger.Error("Element not found");
            throw new Exception("Element not found");
        }

        public void Add(Car car)
        {
            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();
            
            new SQLiteCommand(@$"insert into cars (manufacturer, model, year) 
values('{car.Manufacturer}', '{car.Model}', '{car.Year}')", conn).ExecuteNonQuery();
        }

        public void Update(Car car)
        {
            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();

            new SQLiteCommand(@$"update cars
            set manufacturer = '{car.Manufacturer}',
                model = '{car.Model}',
                year = '{car.Year}'
            where id = '{car.Id}'", conn).ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();

            new SQLiteCommand(@$"delete from cars where id = '{id}'", conn).ExecuteNonQuery();
        }

        public IList<Car> GetByManufacturer(string manufacturer)
        {
            var cars = new List<Car>();

            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();
            using var query = new SQLiteCommand($"select * from cars where manufacturer = {manufacturer}", conn);
            using var reader = query.ExecuteReader();

            while (reader.Read())
            {
                cars.Add(new Car()
                {
                    Id = (int)reader["id"],
                    Manufacturer = reader["manufacturer"].ToString(),
                    Model = reader["model"].ToString(),
                    Year = (int)reader["year"]
                });
            }

            return cars;
        }
    }
}
