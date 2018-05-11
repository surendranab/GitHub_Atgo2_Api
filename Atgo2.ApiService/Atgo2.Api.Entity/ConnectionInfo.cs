namespace Atgo2.Api.Entity
{
    public class ConnectionInfo
    {
        public string TransactionDatabase { get; set; }
        public string LoggingDatabase { get; set; }
        public string RedisDatabase { get; set; }
        public string RedisApp { get; set; }
        public string IntegrationServiceDatabase { get; set; }
    }
}
