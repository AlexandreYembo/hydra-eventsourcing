using EventStore.ClientAPI;

namespace Hydra.EventSourcing.Infrastructure
{
    public interface IEventStoreProvider
    {
         IEventStoreConnection GetConnection();
    }
}