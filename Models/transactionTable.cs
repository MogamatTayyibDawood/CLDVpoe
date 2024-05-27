using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CLDVpoe.Models
{
    public class transactionTable
    {
        public int TransactionID { get; set; } 
        public int UserID { get; set; } 
        public int ProductID { get; set; } 
        public DateTime OrderDate { get; set; } = DateTime.UtcNow; 
        public string OrderStatus { get; set; } 

        public static string ConnectionString = "Server=tcp:tayyibdawood-sql-server.database.windows.net,1433;Initial Catalog=tayyibdawood-sql-database;Persist Security Info=False;User ID=Tayyib;Password=Ilyaas31;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static int InsertTransaction(transactionTable transaction)
        {
            string sql = "INSERT INTO transactionTable (UserID, ProductID, OrderDate, OrderStatus) VALUES (@UserID, @ProductID, @OrderDate, @OrderStatus)";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@UserID", transaction.UserID);
                cmd.Parameters.AddWithValue("@ProductID", transaction.ProductID);
                cmd.Parameters.AddWithValue("@OrderDate", transaction.OrderDate);
                cmd.Parameters.AddWithValue("@OrderStatus", transaction.OrderStatus);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        public static int UpdateOrderStatus(int transactionID, string newStatus)
        {
            string sql = "UPDATE transactionTable SET OrderStatus = @OrderStatus WHERE TransactionID = @TransactionID";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@OrderStatus", newStatus);
                cmd.Parameters.AddWithValue("@TransactionID", transactionID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        public List<transactionTable> GetTransactions(int userId = 0, int productId = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<transactionTable> transactions = new List<transactionTable>();
            string sql = "SELECT * FROM transactionTable";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                if (userId > 0 || productId > 0 || startDate.HasValue || endDate.HasValue)
                {
                    sql += " WHERE ";
                    bool isFirstCondition = true;

                    if (userId > 0)
                    {
                        sql += $" UserID = @UserID";
                        isFirstCondition = false;
                    }

                    if (productId > 0)
                    {
                        if (!isFirstCondition)
                        {
                            sql += " AND ";
                        }
                        sql += $" ProductID = @ProductID";
                        isFirstCondition = false;
                    }

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (!isFirstCondition)
                        {
                            sql += " AND ";
                        }
                        sql += $" OrderDate BETWEEN @StartDate AND @EndDate";
                    }
                    else if (startDate.HasValue)
                    {
                        if (!isFirstCondition)
                        {
                            sql += " AND ";
                        }
                        sql += $" OrderDate >= @StartDate";
                    }
                    else if (endDate.HasValue)
                    {
                        if (!isFirstCondition)
                        {
                            sql += " AND ";
                        }
                        sql += $" OrderDate <= @EndDate";
                    }
                }

                SqlCommand cmd = new SqlCommand(sql, con);

                if (userId > 0)
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                }

                if (productId > 0)
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                }

                if (startDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                }

                if (endDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
                }

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    transactionTable transaction = new transactionTable
                    {
                        TransactionID = Convert.ToInt32(rdr["TransactionID"]),
                        UserID = Convert.ToInt32(rdr["UserID"]),
                        ProductID = Convert.ToInt32(rdr["ProductID"]),
                        OrderDate = Convert.ToDateTime(rdr["OrderDate"]),
                        OrderStatus = rdr["OrderStatus"].ToString()
                    };
                    transactions.Add(transaction);
                }
            }

            return transactions;
        }
    }
}
