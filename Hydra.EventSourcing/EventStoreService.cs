using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace Hydra.EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;
        
        public EventStoreService(IConfiguration configuration)
        {
            var eventSourcingEnabled = bool.Parse(configuration.GetSection("EnableEventSourcing").Value);
            if(eventSourcingEnabled){
                _connection = EventStoreConnection.Create(configuration.GetConnectionString("EventStoreConnection"));
                _connection.ConnectAsync();
            }
        }

        public IEventStoreConnection GetConnection() => _connection;
    }
}