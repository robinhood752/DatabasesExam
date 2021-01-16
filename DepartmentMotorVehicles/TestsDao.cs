using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using log4net;

namespace DepartmentMotorVehicles
{
    public class TestsDao
    {
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public Test Get(int id)
        {
            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();
            using var query = new SQLiteCommand($"select id, manufacturer, model, year from Cars where id = {id}", conn);
            using var reader = query.ExecuteReader();

            if (reader.Read())
            {
                return new Test()
                {
                    Id = (int)reader["id"],
                    CarId = (int)reader["car_id"],
                    IsPassed = (int)reader["is_passed"] != 0,
                    Date = Convert.ToDateTime(reader["date"].ToString())
                };
            }

            Logger.Error("Element not found");
            throw new Exception("Element not found");
        }

        public void Add(Test test)
        {
            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();

            new SQLiteCommand(@$"insert into tests (car_id, is_passed, date)
values('{test.CarId}', '{(test.IsPassed ? 1 : 0)}', '{test.Date.ToString(CultureInfo.InvariantCulture)}')", conn).ExecuteNonQuery();
        }

        public void Update(Test test)
        {
            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();

            new SQLiteCommand(@$"update tests
            set car_id = '{test.CarId}',
                is_passed = '{test.IsPassed}',
                date = '{test.Date}'
            where id = '{test.Id}'", conn).ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString).OpenAndReturn();

            new SQLiteCommand(@$"delete from tests where id = '{id}'", conn).ExecuteNonQuery();
        }

        public List<(int test_id, int car_id, bool isPassed, DateTime date, string manufacturer, string model, int year)>
            GetAllWithCars()
        {
            var results = new List<(int test_id, int car_id, bool is_passed, DateTime date, string manufacturer, string model, int year)>();

            using var conn = new SQLiteConnection(new ConfigurationManager().ConnectionString);
            conn.Open();

            using var query = new SQLiteCommand(@$"select t.id, t.car_id, t.is_passed, t.date, c.manufacturer, c.model, c.year 
from tests t left join cars c on t.car_id = c.id", conn);
            using var reader = query.ExecuteReader();

            while (reader.Read())
            {
                results.Add(((int)reader["id"], 
                    (int)reader["car_id"],
                    (int)reader["is_passed"] != 0,
                    Convert.ToDateTime(reader["date"].ToString()),
                    reader["manufacturer"].ToString(),
                    reader["model"].ToString(),
                    (int) reader["year"]));
            }

            return results;
        }
    }
}
