using System.Data.SqlClient;

namespace CLDVpoe.Models
{
    public class LoginModel
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public static string con_string = "Server=tcp:tayyibdawood-sql-server.database.windows.net,1433;Initial Catalog=tayyibdawood-sql-database;Persist Security Info=False;User ID=Tayyib;Password=Ilyaas31;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int AuthenticateUser(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT UserID, Password FROM userTable WHERE UserEmail = @Email";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string storedPassword = reader.GetString(reader.GetOrdinal("Password"));
                        if (storedPassword == password)
                        {
                            int userId = reader.GetInt32(reader.GetOrdinal("UserID"));
                            reader.Close();
                            return userId;
                        }
                    }
                    reader.Close();
                }
            }
            return -1; // Authentication failed
        }
    }
}