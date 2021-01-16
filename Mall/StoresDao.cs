using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Mall
{
    public class StoresDao
    {
        public List<(long id, string name, int floor, long categoryId)> GetAll()
        {
            var stores = new List<(long id, string name, int floor, long categoryId)>();

            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand("select ID, Name, Floor, CategoryId from Stores", conn);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                stores.Add((
                    (long)reader["ID"], 
                    reader["Name"].ToString(), 
                    (int)reader["Floor"], 
                    (long)reader["CategoryId"]));
            }

            return stores;
        }

        public (long id, string name, int floor, long categoryId) GetById(long id)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand($"select ID, Name, Floor, CategoryId from Stores where ID = {id}", conn);

            var reader = command.ExecuteReader();
            reader.Read();
            
            return ((long)reader["ID"], reader["Name"].ToString(), (int)reader["Floor"], (long)reader["CategoryId"]);
        }

        public void Add((string name, int floor, long categoryId) store)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand(@$"insert into Stores (Name, Floor, CategoryId)
values({store.name}, {store.floor}, {store.categoryId})", conn);

            command.ExecuteNonQuery();
        }

        public void Update((long id, string name, int floor, long categoryId) store)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand(@$"update Stores
set Name = {store.name},
Floor = {store.floor},
CategoryId = {store.categoryId}
where Id = {store.id}", conn);

            command.ExecuteNonQuery();
        }

        public void Delete(long id)
        {
            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand(@$"delete from Stores
where Id = {id}", conn);

            command.ExecuteNonQuery();
        }

        public List<(long id, string name, int floor, long categoryId)> GetAllByCategoryAtFloor(string categoryName, int floor)
        {
            var stores = new List<(long id, string name, int floor, long categoryId)>();

            using var conn = new SqlConnection(new ConfigurationManager().ConnectionString);
            var command = new SqlCommand(@$"select s.ID, s.Name, s.Floor, s.CategoryId
from Stores s left
join Categories c
on s.CategoryId = c.ID
where c.Name = {categoryName}
and s.Floor = {floor}", conn);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                stores.Add((
                    (long)reader["ID"],
                    reader["Name"].ToString(),
                    (int)reader["Floor"],
                    (long)reader["CategoryId"]));
            }

            return stores;
        }
    }
}
