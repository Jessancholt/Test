using Test.HackerNewsProvider.Clients;
using Test.HackerNewsProvider.Clients.Interfaces;

namespace Test.WebAPI.Infrastructure.Configurations
{
    public static class HttpClientsConfiguration
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddScoped<IHackerNewsApi, HackerNewsApi>();
            return services;
        }
    }
}
