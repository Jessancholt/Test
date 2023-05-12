using Test.BusinessLogic.Settings;
using Test.DataAccess.Settings;
using Test.HackerNewsProvider.Settings;

namespace Test.WebAPI.Infrastructure.Configurations
{
    public static class OptionsSettingsConfiguration
    {
        public static WebApplicationBuilder AddSettingsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
            builder.Services.Configure<HackerNewsClientSettings>(builder.Configuration.GetSection("HackerNewsSettings"));
            builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));
            return builder;
        }
    }
}
