using System;
using Imdb.Dao;
using Imdb.Poco;
using log4net;
using Npgsql;

namespace Imdb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GenresDao.Add(new Genre("Drama"));
            GenresDao.Add(new Genre("Comedy"));

            MoviesDao.Add(new Movie("Inherent Vice", new DateTime(2014, 12, 12), 1));
            MoviesDao.Add(new Movie("Knocked Up", new DateTime(2007, 06, 01), 2));
            MoviesDao.Add(new Movie("The Rules of Attraction", new DateTime(2002, 10, 11), 2));
            MoviesDao.Add(new Movie("The Big Lebowski", new DateTime(1998, 03, 06), 2));

            ActorsDao.Add(new Actor("Michelle Anne Sinclair", new DateTime(1981, 05, 21))); ;
            ActorsDao.Add(new Actor("Stephanie Clifford", new DateTime(1979, 03, 17)));
            ActorsDao.Add(new Actor("Aurora Snow", new DateTime(1981, 11, 26)));
            ActorsDao.Add(new Actor("Jessica Steinhauser", new DateTime(1973, 08, 06)));

            MovieActorDao.Add((1, 1));
            MovieActorDao.Add((2, 2));
            MovieActorDao.Add((3, 3));
            MovieActorDao.Add((4, 4));
        }
    }
}
