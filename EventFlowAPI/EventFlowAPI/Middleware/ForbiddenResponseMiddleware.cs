using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Response;
using System.Text.Json;

namespace EventFlowAPI.Middleware
{
    public class ForbiddenResponseMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.ContentType == null &&
                context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                context.Response.ContentType = ContentType.JSON;
                var customResponse = new ForbiddenResponse("Can not acces resource because of insufficient permissions.");

                var responseJson = JsonSerializer.Serialize(customResponse);
                await context.Response.WriteAsync(responseJson);
            }
        }
    }
}
