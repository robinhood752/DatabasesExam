using System;
using System.Collections.Generic;
using Imdb.Poco;
using log4net;
using Npgsql;

namespace Imdb.Dao
{
    public class GenresDao
    {
        private static readonly ILog Logger = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public static List<Genre> Get()
        {
            const string query = "select id, name from dbo.genres";

            var results = new List<Genre>();

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new Genre()
                    {
                        Id = (long)reader["id"],
                        Name = reader["name"].ToString()
                    });
                }

                return results;
            }
            catch (Exception)
            {
                Logger.Error("Failed to get genres");
                return null;
            }
        }

        public static Genre GetById(long id)
        {
            var query = $"select id, name from dbo.genres where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                reader.Read();

                return new Genre()
                {
                    Id = (long)reader["id"],
                    Name = reader["name"].ToString()
                };
            }
            catch (Exception)
            {
                Logger.Error("Failed to get genre");
                return null;
            }
        }

        public static void Add(Genre genre)
        {
            var query = @$"insert into movies (name, release_date, genre_id) values('{genre.Name}')";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to add genre");
            }
        }

        public static void Update(long id, Genre genre)
        {
            var query = @$"update dbo.genres 
set name = '{genre.Name}',
where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to update genre");
            }

        }

        public static void Delete(long id)
        {
            var query = $"delete from dbo.genres where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to delete genre");
            }
        }
    }
}
