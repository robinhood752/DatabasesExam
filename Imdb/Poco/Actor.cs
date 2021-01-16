using System;

namespace Imdb.Poco
{
    public class Actor
    {
        public Actor()
        {
        }

        public Actor(string name, DateTime birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}