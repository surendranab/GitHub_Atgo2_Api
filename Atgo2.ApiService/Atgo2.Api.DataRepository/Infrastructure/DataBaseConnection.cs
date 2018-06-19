using System.Data;
using System.Data.SqlClient;
using Atgo2.Api.Entity;

namespace Atgo2.Api.DataRepository.Repositories.Infrastructure
{
    public sealed class DataBaseConnection : IDataBaseConnection
    {
        public DataBaseConnection(AppSettings appsettings)
        {
            Connection = new SqlConnection(appsettings.connectionInfo.TransactionDatabase);
            IntegrationConnection = new SqlConnection(appsettings.connectionInfo.IntegrationServiceDatabase);
        }
        public IDbConnection Connection { get; set; }
        public IDbConnection IntegrationConnection { get; set; }
    }
}
