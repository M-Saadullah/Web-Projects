using Dapper;
using Microsoft.Data.SqlClient;

namespace The_Outfit.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity> 
    {
        private readonly string _connectionString;

        public GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(TEntity entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "id");

                var columnNames = string.Join(",", properties.Select(p => p.Name));
                var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

                var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames});";

                connection.Execute(query, entity);
            }
        }

        public TEntity GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var primaryKey = "Id";

                var query = $"SELECT * FROM {tableName} WHERE {primaryKey} = @Id;";

                return  connection.QuerySingleOrDefault<TEntity>(query, new { id = id });
                
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;

                var query = $"SELECT * FROM {tableName};";

                return connection.Query<TEntity>(query).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var primaryKey = "id";

                var properties = typeof(TEntity).GetProperties().Where(p => p.Name != primaryKey);

                var setClause = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
                var query = $"UPDATE {tableName} SET {setClause} WHERE {primaryKey} = @{primaryKey};";

                //using (var command = new SqlCommand(query, connection))
                //{
                //    foreach (var property in properties)
                //    {
                //        command.Parameters.AddWithValue("@" + property.Name, property.GetValue(entity));
                //    }
                //    command.Parameters.AddWithValue("@" + primaryKey, typeof(TEntity).GetProperty(primaryKey).GetValue(entity));

                //    command.ExecuteNonQuery();
                //}
                connection.Execute(query, entity);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var primaryKey = "Id";

                var query = $"DELETE FROM {tableName} WHERE {primaryKey} = @Id;";

                connection.Execute(query, new { id = id});
            }
        }

        private TEntity MapReaderToObject(SqlDataReader reader)
        {
            var entity = Activator.CreateInstance<TEntity>();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                property.SetValue(entity, reader[property.Name]);
                //if (property.Name != "Id")
                //{
                //}
            }
            return entity;
        }


    }
}
