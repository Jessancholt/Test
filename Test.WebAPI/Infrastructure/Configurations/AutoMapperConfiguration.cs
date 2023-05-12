using AutoMapper;
using Test.WebAPI.Infrastructure.MappingProfiles;

namespace Test.WebAPI.Infrastructure.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<PostModelMapProfile>();
            });
            
            return services;
        }

        public static WebApplication CheckAutoMapper(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            return app;
        }
    }
}
