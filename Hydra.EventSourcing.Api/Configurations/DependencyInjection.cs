using Hydra.Core.API.User;
using Hydra.Core.Mediator.Abstractions.Mediator;
using Hydra.Core.Mediator.Communication;
using Hydra.Core.Mediator.Integration;
using Hydra.Core.MessageBus.LogEventsIntegrations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.EventSourcing.Api.Configurations
{
   public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
             //API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            //DI for Mediator
            // services.AddScoped<IDispatchLogEventToBus, DispatchLogEventToBus>();
            // services.AddScoped<IMediatorHandler, MediatorHandler>();

            //DI for commands
            // services.AddScoped<IRequestHandler<SaveCustomerCommand, ValidationResult>, CustomerCommandHandler>();

            //DI for Repository
            // services.AddScoped<ICustomerRepository, CustomerRepository>();
            // services.AddScoped<CustomersContext>();
        }
    }
}