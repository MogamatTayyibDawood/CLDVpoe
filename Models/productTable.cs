using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CLDVpoe.Models
{
    public class productTable
    {
        public static readonly string con_string = "Server=tcp:tayyibdawood-sql-server.database.windows.net,1433;Initial Catalog=tayyibdawood-sql-database;Persist Security Info=False;User ID=Tayyib;Password=Ilyaas31;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }
       

        public int InsertProduct(productTable product)
        {
            using (var con = new SqlConnection(con_string))
            {
                try
                {
                    const string sql = "INSERT INTO productTable (Name, Price, Category, Availability) VALUES (@Name, @Price, @Category, @Availability)";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Category", product.Category);
                        cmd.Parameters.AddWithValue("@Availability", product.Availability);
                        

                        con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        public static List<productTable> GetAllProducts()
        {
            var products = new List<productTable>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ProductID, Name, Price, Category, Availability FROM productTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productTable product = new productTable
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        Name = Convert.ToString(reader["Name"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Category = Convert.ToString(reader["Category"]),
                        Availability = Convert.ToString(reader["Availability"]),
                        
                    };
                    products.Add(product);
                }
                reader.Close();
            }

            return products;
        }

    }
}
