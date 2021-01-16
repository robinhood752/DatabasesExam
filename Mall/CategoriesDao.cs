using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Mall
{
    public class CategoriesDao
    {
        public List<(long id, string name)> GetAll()
        {
            var categories = new List<(long id, string name)>();

            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand("select ID, Name from Categories", conn);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add((
                    (long) reader["ID"],
                    reader["Name"].ToString()));
            }

            return categories;
        }

        public (long id, string name) GetById(long id)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand($"select ID, Name from Categories where ID = {id}", conn);

            var reader = command.ExecuteReader();
            reader.Read();

            return ((long)reader["ID"], reader["Name"].ToString());
        }

        public void Add(string categoryName)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand(@$"insert into Categories (Name)
values({categoryName})", conn);

            command.ExecuteNonQuery();
        }
        public void Update((long id, string name) category)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand(@$"update Stores
set Name = {category.name},
WHERE Id = {category.id}", conn);

            command.ExecuteNonQuery();
        }

        public void Delete(long id)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand(@$"delete from Categories
where Id = {id}", conn);

            command.ExecuteNonQuery();
        }

        public (long id, string name) GetTopCategory(long id)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand(@"select ID, Name from Categories
where ID = (select top 1 CategoryId from Stores
group by CategoryId
order by count(id))", conn);

            var reader = command.ExecuteReader();
            reader.Read();

            return ((long)reader["ID"], reader["Name"].ToString());
        }

        
    }
}
