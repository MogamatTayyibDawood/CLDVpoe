using System;
using System.Data.SqlClient;

namespace CLDVpoe.Models
{
    public class userTable
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public static string con_string = "Server=tcp:tayyibdawood-sql-server.database.windows.net,1433;Initial Catalog=tayyibdawood-sql-database;Persist Security Info=False;User ID=Tayyib;Password=Ilyaas31;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static bool CheckExistingEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT COUNT(*) FROM userTable WHERE UserEmail = @Email";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public int InsertUser()
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "INSERT INTO userTable (UserName, UserSurname, UserEmail, Password) VALUES (@UserName, @UserSurname, @UserEmail, @Password)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@UserSurname", UserSurname);
                    cmd.Parameters.AddWithValue("@UserEmail", UserEmail);
                    cmd.Parameters.AddWithValue("@Password", Password); // Store the plain-text password
                    try
                    {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error occurred during user insertion: {ex.Message}");
                        // Log the exception or return a specific error message
                    }
                }
            }
            return rowsAffected;
        }
    }
}