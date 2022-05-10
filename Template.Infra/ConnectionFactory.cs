using System.Data.Common;
using System.Data.SqlClient;
using Template.CrossCutting;

namespace Template.Infra
{
    public class ConnectionFactory
    {

        public static DbConnection GetTemplateOpenConnection() => OpenConnection(ConnectionStrings.TemplateConnection);

        public static DbConnection OpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            return connection;
        }
    }
}
