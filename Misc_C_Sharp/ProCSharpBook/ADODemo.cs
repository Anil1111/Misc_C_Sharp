using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace Misc_C_Sharp.ProCSharpBook
{
    public class ADODemo
    {
        public ADODemo()
        {
            //ConnectionCheck();
            ReadConnectionFromConfig();
        }

        private void ReadConnectionFromConfig()
        {
            string name = "default";
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = settings.ConnectionString;
            Console.WriteLine(conn.ConnectionString);
            conn.Open();
            conn.Close();
        }

        public void ConnectionCheck()
        {
            string connectionString = @"server=(localdb)\MSSQLLocalDb;Database=SPA2;Integrated Security=SSPI;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            connection.Close();
        }
        
    }
}
