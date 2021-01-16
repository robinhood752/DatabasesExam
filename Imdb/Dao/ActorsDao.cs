using System;
using System.Collections.Generic;
using Imdb.Poco;
using log4net;
using Npgsql;

namespace Imdb.Dao
{
    public static class ActorsDao
    {
        private static readonly ILog Logger = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public static List<Actor> Get()
        {
            const string query = "select id, name, birth_date from dbo.actors";

            var results = new List<Actor>();

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new Actor()
                    {
                        Id = (long)reader["id"],
                        Name = reader["name"].ToString(),
                        BirthDate = (DateTime)reader["birth_date"]
                    });
                }

                return results;
            }
            catch (Exception)
            {
                Logger.Error("Failed to get actors");
                return null;
            }
        }

        public static Actor GetById(long id)
        {
            var query = $"select id, name, birth_date from dbo.actors where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                reader.Read();

                return new Actor()
                {
                    Id = (long)reader["id"],
                    Name = reader["name"].ToString(),
                    BirthDate = (DateTime)reader["birth_date"]
                };
            }
            catch (Exception)
            {
                Logger.Error("Failed to get actor");
                return null;
            }
        }

        public static void Add(Actor actor)
        {
            var query = @$"insert into actors (name, birth_date)
values('{actor.Name}', '{actor.BirthDate}')";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to add actor");
            }
        }

        public static void Update(long id, Actor actor)
        {
            var query = @$"update dbo.actors 
set name = '{actor.Name}',
birth_date = '{actor.BirthDate}'
where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to update actor");
            }

        }

        public static void Delete(long id)
        {
            var query = $"delete from dbo.actors where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to delete actor");
            }
        }
    }
}
