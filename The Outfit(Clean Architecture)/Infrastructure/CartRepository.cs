using Microsoft.Data.SqlClient;
using The_Outfit.Models;
using Dapper;
using Application;

namespace The_Outfit.Models
{
    public class CartRepository :ICart, IRepository<CartItems>
    {

        private readonly string _connectionString;
        string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TheOutfit;Integrated Security=True";
        IRepository<CartItems> _repository = new GenericRepository<CartItems>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TheOutfit;Integrated Security=True");

        public CartRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<CartItems> GetAll()
        {
            return _repository.GetAll();
        }
        public CartItems GetById(int id)
        { return _repository.GetById(id);
        }
        public void Delete(int id)
        {

        _repository.Delete(id); 
        }

        public CartItems GetItem(int id, string userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                SELECT * FROM CartItems
                WHERE id = @id AND UserId = @UserId";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CartItems
                        {
                            id = (int)reader["id"],
                            UserId = (string)reader["UserId"],
                            Name = (string)reader["Name"],
                            Price = (decimal)reader["Price"],
                            Discprice = (decimal)reader["Discprice"],
                            Image = (string)reader["Image"],
                            Category = (string)reader["Category"],
                            Quantity = (int)reader["Quantity"]
                        };
                    }
                    else
                    {
                        return null; // Cart item not found
                    }
                }
            }
        }
        public void Add(CartItems cartItem)
        {
            _repository.Add(cartItem);
        }
        public void Update(CartItems cartItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"
                UPDATE CartItems
                SET Quantity = @Quantity
                WHERE id = @id AND UserId = @UserId";
                command.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
                command.Parameters.AddWithValue("@id", cartItem.id);
                command.Parameters.AddWithValue("@UserId", cartItem.UserId);

                command.ExecuteNonQuery();
            }
        }
        public List<CartItems> GetItemsByUser(string userId)
        {
            List<CartItems> cartItems = new List<CartItems>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM CartItems WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CartItems cartItem = new CartItems
                            {
                                UserId = reader["UserId"].ToString(),
                                id = Convert.ToInt32(reader["id"]),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Discprice = Convert.ToDecimal(reader["Discprice"]),
                                Image = reader["Image"].ToString(),
                                Category = reader["Category"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            };
                            cartItems.Add(cartItem);
                        }
                    }
                }
            }
            return cartItems;
        }
        public void Remove(CartItems cartItem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM CartItems WHERE id = @id AND UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", cartItem.id);
                    cmd.Parameters.AddWithValue("@UserId", cartItem.UserId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


