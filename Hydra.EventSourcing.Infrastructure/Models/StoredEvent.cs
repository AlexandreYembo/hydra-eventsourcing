using System;

namespace Hydra.EventSourcing.Infrastructure.Models
{
     /// <summary>
    /// Format of the event you will stored
    /// </summary>
    public class StoredEvent
    {
        public StoredEvent(Guid id, string type, DateTime eventDate, string data)
        {
            this.Id = id;
            this.Type = type;
            this.EventDate = eventDate;
            this.Data = data;

        }
        public Guid Id { get; private set; }

        /// <summary>
        /// Ex: OrderAdded, OrderItemAddded, etc
        /// </summary>
        /// <value></value>
        public string Type { get; private set; }
        public DateTime EventDate { get; private set; }

        /// <summary>
        /// Json serialization - Will convert the data to the object that has the same of Event type. Ex: OrderItemAddded
        /// </summary>
        /// <value></value>
        public string Data { get; private set; }
    }
}