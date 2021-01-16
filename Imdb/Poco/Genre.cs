namespace Imdb.Poco
{
    public class Genre
    {
        public Genre()
        {
        }

        public Genre(string name)
        {
            Name = name;
        }

        public long Id { get; set; }

        public string Name { get; set; }
    }
}
