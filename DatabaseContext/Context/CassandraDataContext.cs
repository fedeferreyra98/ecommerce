using Cassandra;
using Cassandra.Mapping;
using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.Extensions.Configuration;

namespace ecommerce.DatabaseContext.Context;

public class CassandraDataContext : Mappings, IConnection<ISession>
{
    private ISession _session;

    public CassandraDataContext()
    {
        _session = Cluster.Builder()
            .AddContactPoint("localhost") // assuming Cassandra is running on localhost
            .WithPort(9042) // the default Cassandra port is 9042
            .Build()
            .Connect("usercart");
    }
    public ISession GetConnection()
    {
        return _session;
    }
}