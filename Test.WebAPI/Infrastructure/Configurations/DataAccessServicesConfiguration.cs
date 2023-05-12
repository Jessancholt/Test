using Test.DataAccess.Entities;
using Test.DataAccess.Providers;
using Test.DataAccess.Providers.Interfaces;
using Test.DataAccess.Repositories;
using Test.DataAccess.Repositories.Interfaces;
using Test.DataAccess.SqlCommands;
using Test.DataAccess.SqlCommands.Interfaces;
using Test.DataAccess.Wrappers;
using Test.DataAccess.Wrappers.Interfaces;

namespace Test.WebAPI.Infrastructure.Configurations
{
    public static class DataAccessServicesConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISqlRepository<Post>, SqlRepository<Post>>();
            return services;
        }

        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<ISqlProvider<Post>, SqlProvider<Post>>();
            return services;
        }

        public static IServiceCollection AddSqlCommands(this IServiceCollection services)
        {
            services.AddScoped<ISqlCommands<Post>, SqlCommands<Post>>();
            return services;
        }

        public static IServiceCollection AddDbWrappers(this IServiceCollection services)
        {
            services.AddScoped<ISeedWrapper<Post>, SeedWrapper<Post>>();
            services.AddScoped<INonQueryWrapper<Post>, NonQueryWrapper<Post>>();
            services.AddScoped<IDbProviderWrapper<Post>, DbProviderWrapper<Post>>();
            return services;
        }

        public static WebApplication InitializeDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seedService = scope.ServiceProvider.GetRequiredService<ISeedWrapper<Post>>();
            seedService?.CreateTable();

            return app;
        }
    }
}
