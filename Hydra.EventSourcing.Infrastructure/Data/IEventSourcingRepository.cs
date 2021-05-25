using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Core.Mediator.Integration;
using Hydra.EventSourcing.Infrastructure.Models;

namespace Hydra.EventSourcing.Infrastructure.Data
{
   public interface IEventSourcingRepository
    {
         Task SaveEvent(CreateEventSourcingIntegrationEvent tEvent);
         Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}