using EventStore.ClientAPI;

namespace Hydra.EventSourcing
{
    public interface IEventStoreService
    {
         IEventStoreConnection GetConnection();
    }
}