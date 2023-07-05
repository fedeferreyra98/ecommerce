using Cassandra;
using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.Extensions.Configuration;

namespace ecommerce.DatabaseContext.Context;

public class CartCassandraDataContext : IConnection<ISession>
{
    private ISession _session;

    public CartCassandraDataContext(IConfiguration configuration)
    {
        _session = Cluster.Builder()
            .WithCloudSecureConnectionBundle(@"C: \Users\gonza\Documents\Programacion\secure - connect - e - commerce - bd2.zip")
            .WithCredentials(configuration.GetValue<string>("Databases:Cassandra:ClientID"), configuration.GetValue<string>("Databases:Cassandra:ClientSecret"))
            .Build()
            .Connect();

        _session.ChangeKeyspace(configuration.GetValue<string>("Databases:Cassandra:KeySpace"));
    }

    public ISession GetConnection()
    {
        return _session;
    }
}