using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Core.Mediator.Messages;
using Hydra.EventSourcing.Models;

namespace Hydra.EventSourcing.Data
{
   public interface IEventSourcingRepository
    {
         Task SaveEvent<TEvent>(TEvent tEvent) where TEvent : Event;
         Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}