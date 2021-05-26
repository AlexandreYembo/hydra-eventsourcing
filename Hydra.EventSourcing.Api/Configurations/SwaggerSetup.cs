using Hydra.Core.API.Setups;
using Hydra.Core.API.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.EventSourcing.Api.Configurations
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerConfiguration(new SwaggerConfig{Title = "Hydra Event sourcing API", 
                        Description = "This API can be used to get information from Event store or to subscribe an event from bus", 
                        Version = "v1"});
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwaggerConfiguration(new SwaggerConfig{Version = "v1"});
        }
    }
}