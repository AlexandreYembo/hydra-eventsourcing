using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace Hydra.EventSourcing.Infrastructure
{
    public class EventStoreProvider : IEventStoreProvider
    {
        private readonly IEventStoreConnection _connection;
        
        public EventStoreProvider(IConfiguration configuration)
        {
            _connection = EventStoreConnection.Create(configuration.GetConnectionString("EventStoreConnection"));
            _connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection() => _connection;
    }
}