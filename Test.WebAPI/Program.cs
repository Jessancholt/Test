using Test.WebAPI.Infrastructure.Configurations;
using Test.WebAPI.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerialLogger();
builder.AddCustomCors();
builder.AddSettingsConfiguration();
builder.Services.AddAutoMapConfig();
builder.Services.AddSwagger();
builder.Services.AddRepositories();
builder.Services.AddProviders();
builder.Services.AddSqlCommands();
builder.Services.AddDbWrappers();
builder.Services.AddServices();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddHttpClients();
builder.Services.AddControllers();
builder.Services.AddFluentValidation();

var app = builder.Build();

app.CheckAutoMapper();

app.InitializeDatabase();

app.SwaggerConfig();

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("CorsPolicy");

app.ConfigureExceptionHandler();

app.Run();
