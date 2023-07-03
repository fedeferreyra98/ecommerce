using Cassandra;
using Cassandra.Mapping;
using ecommerce.Commerce.Core.Repositories.Contexts.Interfaces;

namespace ecommerce.Commerce.Core.Repositories.Contexts;

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