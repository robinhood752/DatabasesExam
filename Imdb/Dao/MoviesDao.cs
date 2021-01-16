using System;
using System.Collections.Generic;
using Imdb.Poco;
using log4net;
using Npgsql;

namespace Imdb.Dao
{
    public static class MoviesDao
    {
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public static List<Movie> Get()
        { 
            const string query = "select id, name, release_date, genre_id from dbo.movies";

            var results = new List<Movie>();

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new Movie()
                    {
                        Id = (long) reader["id"],
                        Name = reader["name"].ToString(),
                        ReleaseDate = (DateTime) reader["release_date"],
                        GenreId = (long) reader["genre_id"]
                    });
                }

                return results;
            }
            catch (Exception)
            {
                Logger.Error("Failed to get movies");
                return null;
            }
        }

        public static List<Movie> GetWhereActorsSenior()
        {
            const string query = @"select distinct m.id, m.name, m.release_date, m.genre_id from dbo.movies m
join dbo.movies_actors ma on m.id = ma.movie_id 
join dbo.actors a on ma.actor_id = a.id
where extract(year from a.birth_date) < 1972";

            var results = new List<Movie>();

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new Movie()
                    {
                        Id = (long)reader["id"],
                        Name = reader["name"].ToString(),
                        ReleaseDate = (DateTime)reader["release_date"],
                        GenreId = (long)reader["genre_id"]
                    });
                }

                return results;
            }
            catch (Exception)
            {
                Logger.Error("Failed to get movies");
                return null;
            }
        }

        

        public static Movie GetById(long id)
        {
            var query = $"select id, name, release_date, genre_id from dbo.movies where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                reader.Read();

                return new Movie()
                {
                    Id = (long) reader["id"],
                    Name = reader["name"].ToString(),
                    ReleaseDate = (DateTime) reader["release_date"],
                    GenreId = (long) reader["genre_id"]
                };
            }
            catch (Exception)
            {
                Logger.Error("Failed to get movie");
                return null;
            }
        }

        public static void Add(Movie movie)
        {
            var query = @$"insert into dbo.movies (name, release_date, genre_id)
values('{movie.Name}', '{movie.ReleaseDate}', {movie.GenreId})";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to add movie");
            }
        }

        public static void Update(long id, Movie movie)
        {
            var query = @$"update dbo.movies 
set name = '{movie.Name}',
release_date = '{movie.ReleaseDate}'
genre_id = {movie.GenreId}
where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to update movie");
            }

        }

        public static void Delete(long id)
        {
            var query = $"delete from dbo.movies where id = {id}";

            try
            {
                using var connection = new NpgsqlConnection(new ConfigurationManager().ConnectionString);
                var command = new NpgsqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Logger.Error("Failed to delete movie");
            }
        }
    }
}
