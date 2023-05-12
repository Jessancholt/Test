using Test.BusinessLogic.Services;
using Test.BusinessLogic.Services.Interfaces;
using Test.WebAPI.Models.ApiResponses;

namespace Test.WebAPI.Infrastructure.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<IHackerNewsService, HackerNewsService>();
            services.AddScoped<ICacheService<string, PostResponse>, CacheService<string, PostResponse>>();
            return services;
        }
    }
}
