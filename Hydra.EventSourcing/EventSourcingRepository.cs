using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Hydra.Core.Data.EventSourcing;
using Hydra.Core.Messages;
using Newtonsoft.Json;

namespace Hydra.EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task SaveEvent<TEvent>(TEvent tEvent) where TEvent : Event
        {
            await _eventStoreService.GetConnection().AppendToStreamAsync(
                tEvent.AggregateId.ToString(),
                ExpectedVersion.Any,
                FormatEvent(tEvent));
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
        {
            var events = await _eventStoreService.GetConnection()
                .ReadStreamEventsForwardAsync(aggregateId.ToString(), 0, 500, false);

            var listEvents = new List<StoredEvent>();

            foreach (var resolvedEvent in events.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

                var evento = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded);

                listEvents.Add(evento);
            }

            return listEvents.OrderBy(e => e.EventDate);
        }

        private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent evento) where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                evento.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)),
                null);
        }
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}