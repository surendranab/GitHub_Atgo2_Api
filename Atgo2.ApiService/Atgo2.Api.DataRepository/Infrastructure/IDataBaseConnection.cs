using System.Data;

namespace Atgo2.Api.DataRepository.Repositories.Infrastructure
{
    public interface IDataBaseConnection
    {
        IDbConnection Connection { get; }
    }
}
