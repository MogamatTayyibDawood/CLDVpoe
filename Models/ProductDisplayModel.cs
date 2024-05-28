using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CLDVpoe.Models
{
    public class ProductDisplayModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }

        public static List<ProductDisplayModel> SelectProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();

            string con_string = "Server=tcp:tayyibdawood-sql-server.database.windows.net,1433;Initial Catalog=tayyibdawood-sql-database;Persist Security Info=False;User ID=Tayyib;Password=Ilyaas31;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ProductID, Name, Price, Category, Availability FROM productTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplayModel product = new ProductDisplayModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        Name = Convert.ToString(reader["Name"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Category = Convert.ToString(reader["Category"]),
                        Availability = Convert.ToString(reader["Availability"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
    }
}
