using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Response;
using System.Text.Json;

namespace EventFlowAPI.Middleware
{
    public class UnauthorizedResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.ContentType == null &&
                context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = ContentType.JSON;
                var customResponse = new UnauthorizedResponse("Please log in to access this resource.");

                var responseJson = JsonSerializer.Serialize(customResponse);
                await context.Response.WriteAsync(responseJson);
            }
        }
    }
}
