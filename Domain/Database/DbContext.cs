using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
namespace Domain.Database
{
    public class DbContext
    {
        private readonly IEnumerable<Type> tables;

        public DbContext()
        {
            tables = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes()).Where(t => t.IsClass && t.Namespace == "Domain.Database.Tables");
        }

        private void CreateTables()
        {
            for (int i = 0; i < tables.Count(); i++)
            {
                OpenConnection().CreateTable(tables.ElementAt(i));
            }
        }

        private void DeleteTables()
        {
            var conn = OpenConnection();

            for (int i = 0; i < conn.TableMappings.Count(); i++)
            {
                var tableMap = conn.TableMappings.ElementAt(i);
                conn.DropTable(tableMap);
            }
        }

        public SQLiteConnection OpenConnection() => new SQLiteConnection(DbPath.String());

        public void OnCreate()
        {
            CreateTables();
        }

        public void OnDelete()
        {
            DeleteTables();
        }

        public void Add<T>(T item) => OpenConnection().Insert(item, typeof(T));

        public void Remove<T>(object primaryKey) => OpenConnection().Delete<T>(primaryKey);

        public void Remove(object item) => OpenConnection().Delete(item);

        public void Update<T>(T item) => OpenConnection().Update(item, typeof(T));

        public int AddMany<T>(IEnumerable<T> items) => OpenConnection().InsertAll(items);

        public T Get<T>(object primaryKey)
        {
            var conn = OpenConnection();

            var item = conn.Get(primaryKey, conn.GetMapping(typeof(T)));

            return (T)item;
        }

        public TableQuery<T> GetAll<T>() where T : new()
        {
            var conn = OpenConnection();

            return conn.Table<T>();
        }
    }
}
