using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanArchitecture.Api.Middleware;

public static class ExceptionHandlerMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var feature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = feature?.Error;

                if (exception is null)
                    return;

                var problem = new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    title = "An error occurred while processing your request.",
                    status = context.Response.StatusCode,
                    detail = context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true
                        ? exception.ToString()
                        : "An unexpected error has occurred."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(problem, JsonOptions));
            });
        });
    }
}
