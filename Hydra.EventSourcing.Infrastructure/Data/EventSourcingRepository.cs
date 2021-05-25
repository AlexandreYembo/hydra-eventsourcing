using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Hydra.Core.Mediator.Integration;
using Hydra.Core.Mediator.Messages;
using Hydra.EventSourcing.Infrastructure.Models;
using Newtonsoft.Json;

namespace Hydra.EventSourcing.Infrastructure.Data
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreProvider _eventStoreProvider;

        public EventSourcingRepository(IEventStoreProvider eventStoreProvider)
        {
            _eventStoreProvider = eventStoreProvider;
        }

        public async Task SaveEvent(CreateEventSourcingIntegrationEvent tEvent)
        {
            await _eventStoreProvider.GetConnection().AppendToStreamAsync(
                tEvent.AggregateId.ToString(),
                ExpectedVersion.Any,
                FormatEvent(tEvent));
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
        {
            var events = await _eventStoreProvider.GetConnection()
                .ReadStreamEventsForwardAsync(aggregateId.ToString(), 0, 500, false);

            var listEvents = new List<StoredEvent>();

            foreach (var resolvedEvent in events.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

                var tEvent = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded);

                listEvents.Add(tEvent);
            }

            return listEvents.OrderBy(e => e.EventDate);
        }

        private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent tEvent) where TEvent : CreateEventSourcingIntegrationEvent
        {
            yield return new EventData(
                Guid.NewGuid(),
                tEvent.MessageType,
                true,
                Encoding.UTF8.GetBytes(tEvent.Entity),
                null);
        }
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}
