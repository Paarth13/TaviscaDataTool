using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
   public class SqlConnector
    {
        private SqlConnection connector;
        private SqlConnection Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            connector = new SqlConnection(connectionString);
            return connector;
        }
        public SqlConnection ConnectionEstablisher()
        {
            return Connection();
        }
    }
}
