using System.Configuration;
using System.Data;
using System.Data.Common;

namespace TesteDapperRepository.Infraestrutura
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DapperContext"].ConnectionString;

        public IDbConnection GetConnection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                return connection;
            }
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
