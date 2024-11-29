using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Response;
using Serilog;
using System.Text.Json;

namespace EventFlowAPI.Middleware
{
    public class UnauthorizedResponseMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            //Log.Information($"Path {context.Request.Path}");
            if (context.Response.ContentType == null &&
                context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                Log.Information("UnauthorizedResponse");
                context.Response.ContentType = ContentType.JSON;
                var customResponse = new UnauthorizedResponse("Please log in to access this resource.");

                var responseJson = JsonSerializer.Serialize(customResponse);
                await context.Response.WriteAsync(responseJson);
            }
        }
    }
}
