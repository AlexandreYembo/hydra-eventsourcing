using System;
using System.Threading.Tasks;
using Hydra.Core.API.Controllers;
using Hydra.EventSourcing.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.EventSourcing.Api.Controllers
{
    public class EventStoreController : MainController
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public EventStoreController(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }

       [HttpGet("events/{id:guid}")]
        public async Task<IActionResult> GetEvents(Guid id)
        {
            var events = await _eventSourcingRepository.GetEvents(id);
            return CustomResponse(events);
        }
    }
}