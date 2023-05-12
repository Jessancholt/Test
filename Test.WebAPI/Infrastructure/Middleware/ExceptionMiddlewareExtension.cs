using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Test.WebAPI.Infrastructure.Middleware
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature is not null)
                    {
                        switch (contextFeature.Error)
                        {
                            case BadHttpRequestException:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                await context.Response.WriteAsJsonAsync(contextFeature.Error.Message);
                                break;

                            case NullReferenceException:
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                await context.Response.WriteAsJsonAsync(contextFeature.Error.Message);
                                break;

                            default:
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                await context.Response.WriteAsJsonAsync(contextFeature.Error.Message);
                                break;
                        }
                    }
                });
            });
        }
    }
}
