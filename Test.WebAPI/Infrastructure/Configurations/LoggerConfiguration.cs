using Serilog;

namespace Test.WebAPI.Infrastructure.Configurations
{
    public static class LoggerConfiguration
    {
        public static WebApplicationBuilder AddSerialLogger(this WebApplicationBuilder appBuilder)
        {
            appBuilder.Host.UseSerilog((_, _, config) =>
            {
                config = config.WriteTo.Console();
                config = appBuilder.Environment.IsDevelopment()
                    ? config.MinimumLevel.Debug()
                    : config.MinimumLevel.Warning();
            });
            return appBuilder;
        }
    }
}
