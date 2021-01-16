using System;
using System.Collections.Generic;
using log4net;
using Npgsql;

namespace ConstructionCompany
{
    public class Program
    {
        private static readonly ILog Logger = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public static void Main(string[] args)
        {
            var workers = WorkersWithRoles(new ConfigurationManager().ConnectionString);

            workers.ForEach(worker => Console.WriteLine(worker.ToString()));
        }

        private static List<Worker> WorkersWithRoles(string connectionString)
        {
            var workers = new List<Worker>();

            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                var command = new NpgsqlCommand("workers_with_roles", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    workers.Add(new Worker
                    {
                        Name = reader["name"].ToString(),
                        Phone =  reader["phone"].ToString(),
                        Salary = (int)reader["salary"]
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return workers;
        }
    }
}