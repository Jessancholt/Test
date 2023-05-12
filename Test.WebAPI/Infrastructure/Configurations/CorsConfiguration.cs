namespace Test.WebAPI.Infrastructure.Configurations
{
    public static class CorsConfiguration
    {
        public static WebApplicationBuilder AddCustomCors(this WebApplicationBuilder builder)
        {
            var origins = builder.Configuration["AllowedOrigins"].Split(";");
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            return builder;
        }
    }
}
