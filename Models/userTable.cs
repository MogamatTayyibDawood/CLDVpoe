using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CLDVpoe.Models
{
    public class userTable : Controller
    {

        public static string con_string = "Server=tcp:tayyibdawood-sql-server.database.windows.net,1433;Initial Catalog=tayyibdawood-sql-database;Persist Security Info=False;User ID=Tayyib;Password=Ilyaas31;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlConnection con = new SqlConnection(con_string);

        public IActionResult Index()
        {
            return View();
        }
    }
}
