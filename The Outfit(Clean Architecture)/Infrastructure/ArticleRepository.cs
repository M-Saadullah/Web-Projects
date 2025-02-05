using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace The_Outfit.Models
{
    public class ArticleRepository :IArticle
    {
        private readonly string connString;
        private readonly IRepository<Article> _repository;
        //string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"TheOutfit\";Integrated Security=True;";
        //IRepository<Article> _repository = new GenericRepository<Article>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"TheOutfit\";Integrated Security=True;");
        //List<Article> articles = new List<Article>();

        public ArticleRepository(string connString , IRepository<Article> repo)
        {
            this.connString = connString;   
            this._repository = repo;

        }


        public int GetStockQuantity(int productId)
        {
            int stockQuantity = 0;

            string query = "SELECT Quantity FROM Article WHERE id = @ProductId";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    stockQuantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                }
            }

            return stockQuantity;
        }


        public void UpdateStockQuantity(int productId, int newQuantity)
        {
            string query = "UPDATE Article SET Quantity = @NewQuantity WHERE id = @ProductId";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@NewQuantity", newQuantity);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Add(Article article)
        {

            //SqlConnection connection = new SqlConnection(connString);
            //connection.Open();
            //string query = $"insert into Article(Name,color_details,Price,Quantity,Category,Features,Images)values(@name,@color_details,@price,@quantity,@category,@features,@images)";
            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("name", article.Name);
            //cmd.Parameters.AddWithValue("color_details", article.colorDetails);
            //cmd.Parameters.AddWithValue("price", article.Price);
            //cmd.Parameters.AddWithValue("Quantity", article.Quantity);
            //cmd.Parameters.AddWithValue("Category", article.Category);
            //cmd.Parameters.AddWithValue("Features", article.Features);
            //cmd.Parameters.AddWithValue("Images", article.Images);
            //cmd.ExecuteNonQuery();
            //connection.Close();


            _repository.Add(article);
        }

        [HttpPost]
        public Article EditArticle(int ID)
        {
            //SqlConnection connection = new SqlConnection(connString);
            //connection.Open();
            //string query = $"select * from Article where Id = @id"; // Use parameter

            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@id", ID); // Add name as parameter

            //SqlDataReader reader = cmd.ExecuteReader();

            //Article article = null;
            //if (reader.HasRows)
            //{
            //    reader.Read(); // Read the first row if data exists
            //    article = new Article();
            //    article.Id = Convert.ToInt32(reader[0]);
            //    article.Name = reader["Name"].ToString();
            //    article.colorDetails = reader["color_details"].ToString();
            //    article.Price = Convert.ToDecimal(reader["Price"]);
            //    article.Quantity = Convert.ToInt32(reader["Quantity"]);
            //    article.Category = reader["Category"].ToString();
            //    article.Features = reader["Features"].ToString();
            //    article.Images = reader["Images"].ToString();
            //}

            //reader.Close();
            //connection.Close();
            //return article;


            //_repository.Update(article);

            Article article = null;
            article = _repository.GetById(ID);
            return article;

        }

        public void Update(Article article)
        {
            //SqlConnection connection = new SqlConnection(connString);
            //connection.Open();
            //string query = $"UPDATE Article SET Name = @name, color_details = @color_details, Price = @price, Quantity = @quantity, Category = @category, Features = @features WHERE Id = @id";
            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("id", article.Id);
            //cmd.Parameters.AddWithValue("name", article.Name);
            //cmd.Parameters.AddWithValue("color_details", article.colorDetails);
            //cmd.Parameters.AddWithValue("price", article.Price);
            //cmd.Parameters.AddWithValue("Quantity", article.Quantity);
            //cmd.Parameters.AddWithValue("Category", article.Category);
            //cmd.Parameters.AddWithValue("Features", article.Features);
            //cmd.ExecuteNonQuery();
            //connection.Close();

            _repository.Update(article);


      

        }

        public void Delete(int ID)
        {
            //SqlConnection connection = new SqlConnection(connString);
            //connection.Open();
            //string query = $"DELETE FROM Article WHERE Id = @id";
            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("id", ID);
            //int c = cmd.ExecuteNonQuery();
            //connection.Close();

            _repository.Delete(ID);


        }


        public Article GetById(int ID)
        {
            //SqlConnection connection = new SqlConnection(connString);
            //connection.Open();
            //string query = $"select * from Article where Id = @id"; // Use parameter

            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@id", ID); // Add name as parameter

            //SqlDataReader reader = cmd.ExecuteReader();

            //Article article = null;
            //if (reader.HasRows)
            //{
            //    reader.Read(); // Read the first row if data exists
            //    article = new Article();
            //    article.Id = Convert.ToInt32(reader[0]);
            //    article.Name = reader["Name"].ToString();
            //    article.colorDetails = reader["color_details"].ToString();
            //    article.Price = Convert.ToDecimal(reader["Price"]);
            //    article.Quantity = Convert.ToInt32(reader["Quantity"]);
            //    article.Category = reader["Category"].ToString();
            //    article.Features = reader["Features"].ToString();
            //    article.Images = reader["Images"].ToString();
            //}

            //reader.Close();
            //connection.Close();
            //return article;
            Article article = null;
            article = _repository.GetById(ID);
            return article;

        }



        public List<Article> get(string categry)
        {
            List<Article> articles = new List<Article>();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = $"select * from article where category = @Categorey";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("Categorey", categry);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Article article = new Article();

                article.id = Convert.ToInt32(reader[0]);
                article.Name = reader["Name"].ToString();
                article.Price = Convert.ToDecimal(reader["Price"]);
                article.Category = reader["Category"].ToString();
                article.Features = reader["Features"].ToString();
                article.Images = reader["Images"].ToString();

                articles.Add(article);
            }
            return articles;
        }


        public IEnumerable<Article> GetAll()
        {
            //SqlConnection connection = new SqlConnection(connString);
            //connection.Open();
            //string query = "select * from article";
            //SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Article article = new Article();
            //    article.Id = Convert.ToInt32(reader[0]);
            //    article.Name = reader["Name"].ToString();
            //    article.colorDetails = reader["color_details"].ToString();
            //    article.Price = Convert.ToDecimal(reader["Price"]);
            //    article.Quantity = Convert.ToInt32(reader["Quantity"]);
            //    article.Category = reader["Category"].ToString();
            //    article.Features = reader["Features"].ToString();
            //    article.Images = reader["Images"].ToString();

            //    articles.Add(article);
            //}
            //return articles;

            return _repository.GetAll();

        }


        public Article GetItem(int id)
        {
            //Article article = new Article();

            //using (SqlConnection connection = new SqlConnection(connString))
            //{
            //    connection.Open();

            //    string query = "SELECT * FROM article WHERE Id = @id";
            //    SqlCommand cmd = new SqlCommand(query, connection);
            //    cmd.Parameters.AddWithValue("@id", ID);

            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        if (reader.HasRows)
            //        {
            //            reader.Read();
            //            article.Name = reader.GetString("Name");
            //            article.Price = reader.GetDecimal("Price");
            //            article.Quantity = reader.GetInt32("Quantity");
            //            article.Category = reader.GetString("Category");
            //            article.Features = reader.GetString("Features");
            //            article.Images = reader.GetString("Images");
            //            article.colorDetails = reader.GetString("color_details");
            //        }
            //        else
            //        {
            //            // Handle case where no article is found with the ID
            //        }
            //    }
            //}

            //return article;

            Article article = null;
            article = _repository.GetById(id);
            return article;
        }
    }
}


//using Microsoft.CodeAnalysis;
//using Microsoft.Data.SqlClient;
//using System.Data;
//using System.Data.SqlClient;
//namespace RJTECH_Authentication_.Models
//{
//    public class articleRepository
//    {
//        string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RJTECHDB;Integrated Security=True";
//        List<article> articles = new List<article>();
//        public void Add(article article)
//        {
//            SqlConnection connection = new SqlConnection(connect);
//            connection.Open();
//            string query = $"insert into article(Name,PakageDetail,Price,Quantity,Category,Feature,Images)values(@name,@pakagedetail,@price,@quantity,@category,@feature,@images)";
//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("name", article.Name);
//            cmd.Parameters.AddWithValue("pakagedetail", article.PakageDetail);
//            cmd.Parameters.AddWithValue("price", article.Price);
//            cmd.Parameters.AddWithValue("Quantity", article.Quantity);
//            cmd.Parameters.AddWithValue("Category", article.Category);
//            cmd.Parameters.AddWithValue("Feature", article.Feature);
//            cmd.Parameters.AddWithValue("Images", article.Images);
//            cmd.ExecuteNonQuery();
//            connection.Close();


//        }
//        public article detail(int ID)
//        {
//            SqlConnection connection = new SqlConnection(connect);
//            connection.Open();
//            string query = $"select * from article where id = @id"; // Use parameter

//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("@id", ID); // Add name as parameter

//            SqlDataReader reader = cmd.ExecuteReader();

//            article article = null;
//            if (reader.HasRows)
//            {
//                reader.Read(); // Read the first row if data exists
//                article = new article();
//                article.Id = Convert.ToInt32(reader[0]);
//                article.Name = reader["Name"].ToString();
//                article.PakageDetail = reader["PakageDetail"].ToString();
//                article.Price = Convert.ToDecimal(reader["Price"]);
//                article.Quantity = Convert.ToInt32(reader["Quantity"]);
//                article.Category = reader["Category"].ToString();
//                article.Feature = reader["Feature"].ToString();
//                article.Images = reader["Images"].ToString();
//            }

//            reader.Close();
//            connection.Close();
//            return article;
//        }

//        public article Edit(int ID)
//        {
//            SqlConnection connection = new SqlConnection(connect);
//            connection.Open();
//            string query = $"select * from article where id = @id"; // Use parameter

//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("@id", ID); // Add name as parameter

//            SqlDataReader reader = cmd.ExecuteReader();

//            article article = null;
//            if (reader.HasRows)
//            {
//                reader.Read(); // Read the first row if data exists
//                article = new article();
//                article.Id = Convert.ToInt32(reader[0]);
//                article.Name = reader["Name"].ToString();
//                article.PakageDetail = reader["PakageDetail"].ToString();
//                article.Price = Convert.ToDecimal(reader["Price"]);
//                article.Quantity = Convert.ToInt32(reader["Quantity"]);
//                article.Category = reader["Category"].ToString();
//                article.Feature = reader["Feature"].ToString();
//                article.Images = reader["Images"].ToString();
//            }

//            reader.Close();
//            connection.Close();
//            return article;

//        }
//        public void Update(article article)
//        {
//            SqlConnection connection = new SqlConnection(connect);
//            connection.Open();
//            string query = $"UPDATE article SET Name = @name, PakageDetail = @pakagedetail, Price = @price, Quantity = @quantity, Category = @category, Feature = @feature WHERE id = @id";
//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("id", article.Id); // Assuming id is the ID of the article to be updated
//            cmd.Parameters.AddWithValue("name", article.Name);
//            cmd.Parameters.AddWithValue("pakagedetail", article.PakageDetail);
//            cmd.Parameters.AddWithValue("price", article.Price);
//            cmd.Parameters.AddWithValue("quantity", article.Quantity);
//            cmd.Parameters.AddWithValue("category", article.Category);
//            cmd.Parameters.AddWithValue("feature", article.Feature);
//            cmd.ExecuteNonQuery();
//            connection.Close();


//        }
//        public void Remove(int ID)
//        {
//            SqlConnection connection = new SqlConnection(connect);
//            connection.Open();
//            string query = $"DELETE FROM article WHERE id = @id";
//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("id", ID);
//            int c = cmd.ExecuteNonQuery();
//            connection.Close();

//        }
//        public List<article> get(string categry)
//        {
//            List<article> articles = new List<article>();
//            SqlConnection connection = new SqlConnection(connect);
//            connection.Open();
//            string query = $"select * from article where category = @Categorey";

//            SqlCommand cmd = new SqlCommand(query, connection);
//            cmd.Parameters.AddWithValue("Categorey", categry);
//            SqlDataReader reaader = cmd.ExecuteReader();
//            while (reaader.Read())
//            {
//                article article = new article();
//                article.Id = Convert.ToInt32(reaader[0]);
//                article.Name = reaader["Name"].ToString();
//                article.Price = Convert.ToDecimal(reaader["Price"]);
//                article.Quantity = Convert.ToInt32(reaader["Quantity"]);
//                article.Category = reaader["Category"].ToString();
//                article.Images = reaader["Images"].ToString();

//                articles.Add(article);
//            }
//            return articles;
//        }
//        public List<article> GetAll()
//        {
//            SqlConnection connection = new SqlConnection(connect);
//            connection.Open();
//            string query = "select * from article";
//            SqlCommand cmd = new SqlCommand(query, connection);
//            SqlDataReader reaader = cmd.ExecuteReader();
//            while (reaader.Read())
//            {
//                article article = new article();
//                article.Id = Convert.ToInt32(reaader[0]);
//                article.Name = reaader["Name"].ToString();
//                article.PakageDetail = reaader["PakageDetail"].ToString();
//                article.Price = Convert.ToDecimal(reaader["Price"]);
//                article.Quantity = Convert.ToInt32(reaader["Quantity"]);
//                article.Category = reaader["Category"].ToString();
//                article.Feature = reaader["Feature"].ToString();
//                article.Images = reaader["Images"].ToString();

//                articles.Add(article);
//            }
//            return articles;
//        }
//        public article GetItem(int ID)
//        {
//            article article = new article();

//            using (SqlConnection connection = new SqlConnection(connect))
//            {
//                connection.Open();

//                string query = "SELECT * FROM article WHERE id = @id";
//                SqlCommand cmd = new SqlCommand(query, connection);
//                cmd.Parameters.AddWithValue("@id", ID);

//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    if (reader.HasRows)
//                    {
//                        reader.Read();
//                        article.Name = reader.GetString("Name");
//                        article.Price = reader.GetDecimal("Price");
//                        article.Quantity = reader.GetInt32("Quantity");
//                        article.Category = reader.GetString("Category");
//                        article.Feature = reader.GetString("Feature");
//                        article.Images = reader.GetString("Images");
//                        article.PakageDetail = reader.GetString("PakageDetail");
//                    }
//                    else
//                    {
//                        // Handle case where no article is found with the ID
//                    }
//                }
//            }

//            return article;
//        }

//    }
//}