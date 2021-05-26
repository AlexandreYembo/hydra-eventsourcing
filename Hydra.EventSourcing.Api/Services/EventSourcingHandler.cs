using System;
using System.Threading;
using System.Threading.Tasks;
using Hydra.Core.Mediator.Integration;
using Hydra.Core.MessageBus;
using Hydra.EventSourcing.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hydra.EventSourcing.Api.Services
{
    public class EventSourcingHandler :  BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public EventSourcingHandler(IMessageBus messageBus, 
                                    IServiceProvider serviceProvider                                   )
        {
            _serviceProvider = serviceProvider;
            _messageBus = messageBus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscriber();
            return Task.CompletedTask;
        }

        private void SetSubscriber() => _messageBus.SubscribeAsync<CreateEventSourcingIntegrationEvent>("EventSourcingService", async request =>
                                        await DispatchEventSource(request));

        private async Task DispatchEventSource(CreateEventSourcingIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var eventSourcingRepository = scope.ServiceProvider.GetRequiredService<IEventSourcingRepository>();

            if(message.AggregateId == default(Guid)) return;
            
            await eventSourcingRepository.SaveEvent(message);
        }
    }
}