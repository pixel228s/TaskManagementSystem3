using IssueManagement.Infrastructure.Middlewares;
using IssueManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace IssueManagement.Extensions
{
    public static class ErrorHandlingExtenstion
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server error"
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
