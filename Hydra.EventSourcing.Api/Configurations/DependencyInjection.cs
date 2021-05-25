using Hydra.Core.API.User;
using Hydra.Core.Mediator.Abstractions.Mediator;
using Hydra.Core.Mediator.Communication;
using Hydra.EventSourcing.Infrastructure;
using Hydra.EventSourcing.Infrastructure.Data;
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

             // Event Sourcing
            services.AddSingleton<IEventStoreProvider, EventStoreProvider>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
            
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }
    }
}