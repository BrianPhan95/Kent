using Kent.Libary.Configurations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Kent.Entities.Repositories
{
    public class BaseRepository
    {
        internal IDbConnection Connection
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings[KentConfiguration.ConnectionString].ConnectionString;
                return new SqlConnection(connectionString);
            }
        }
    }
}
