using System;
using System.Collections.Generic;
using log4net;
using Npgsql;

namespace Imdb.Poco
{
    public class MovieActorDao
    {
        private static readonly ILog Logger = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public static List<(long id, long movieId, long actorId)?> Get()
        {
            const string query = "select id, movie_id, actor_id from dbo.movies_actors";

            var results = new List<(long id, long movieId, long actorId)?>();

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(((long)reader["id"], (long)reader["movie_id"], (long)reader["actor_id"]));
                }

                return results;
            }
            catch (Exception)
            {
                Logger.Error("Failed to get movie actor pairs");
                return null;
            }
        }

        public static (long id, long movieId, long actorId)? GetById(long id)
        {
            var query = $"select id, movie_id, actor_id from dbo.movies_actors where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                reader.Read();

                return ((long) reader["id"], (long) reader["movie_id"], (long) reader["actor_id"]);
            }
            catch (Exception)
            {
                Logger.Error("Failed to get movie actor pair");
                return null;
            }
        }

        public static void Add((long movieId, long actorId)  movieActorPair)
        {
            var (movieId, actorId) = movieActorPair;
            var query = @$"insert into dbo.movies_actors (name, release_date, genre_id)
values('{movieId}', '{actorId}')";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to add movie_actor_pair");
            }
        }

        public static void Update(long id, (long movieId, long actorId) movieActorPair)
        {
            var (movieId, actorId) = movieActorPair;
            var query = @$"update dbo.movies_actors
set movie_id = '{movieId}',
actor_id = '{actorId}'
where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to update movie actor pair");
            }

        }

        public static void Delete(long id)
        {
            var query = $"delete from dbo.movies_actors where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to delete movie actor pair");
            }
        }
    }
}
