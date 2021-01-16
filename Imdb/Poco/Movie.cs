using System;

namespace Imdb.Poco
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(string name, DateTime releaseDate, long genreId)
        {
            Name = name;
            ReleaseDate = releaseDate;
            GenreId = genreId;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public long GenreId { get; set; }
    }
}
