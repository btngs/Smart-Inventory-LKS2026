using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIntangSyahriMahardika_Module2
{
    public static class koneksi
    {
        public static string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Inventory_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connString);
        }
    }

    public static class userSession
    {
        public static string username { get; set; }
    }
}
    