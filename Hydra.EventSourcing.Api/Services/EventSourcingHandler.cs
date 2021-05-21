using System;
using System.Threading;
using System.Threading.Tasks;
using Hydra.Core.Mediator.Abstractions.Mediator;
using Hydra.Core.Mediator.Integration;
using Hydra.Core.Mediator.Messages;
using Hydra.Core.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hydra.EventSourcing.Api.Services
{
    public class EventSourcingHandler :  BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public EventSourcingHandler(IMessageBus messageBus, 
                                    IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _messageBus = messageBus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscriber();
            return Task.CompletedTask;
        }

        private void SetSubscriber() => _messageBus.SubscribeAsync<CreateEventSourcingIntegrationEvent>("CreateEventSourcing", async request =>
                                        await DispatchEventSource(request));

        private Task DispatchEventSource(CreateEventSourcingIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

            return Task.CompletedTask;

            // var orderItemRemovedStock = new OrderItemRemovedFromStockIntegrationEvent(message.CustomerId, message.OrderId);
            // await _messageBus.PublishAsync(orderItemRemovedStock); //Payment API will subscribe this event
        }
    }
}