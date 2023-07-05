using Cassandra;
using Cassandra.Mapping;
using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.Extensions.Configuration;

namespace ecommerce.DatabaseContext.Context;

public class CassandraDataContext : Mappings, IConnection<ISession>
{
    private ISession _session;

    public CassandraDataContext(IConfiguration configuration)
    {
        _session = Cluster.Builder()
            .WithCloudSecureConnectionBundle(@"string donde sta el archivo secure-connect-e-commerce")
            .WithCredentials(configuration.GetValue<string>("Databases:Cassandra:ClientID"),
                configuration.GetValue<string>("Databases:Cassandra:ClientSecret"))
            .Build()
            .Connect();
    }
    public ISession GetConnection()
    {
        return _session;
    }
}