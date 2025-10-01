
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Response;
using System.Text.Json;

namespace EventFlowAPI.Middleware
{
    public class MethodNotAllowedResponseMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.ContentType == null &&
                context.Response.StatusCode == StatusCodes.Status405MethodNotAllowed)
            {
                var httpMethod = context.Request.Method;
                if (httpMethod == HttpMethods.Patch)
                {
                    context.Response.ContentType = ContentType.JSON;
                    var customResponse = new MethodNotAllowedResponse("The PATCH method is not allowed for this resource. Allowed HTTP methods are GET, POST, PUT, and DELETE.");

                    var responseJson = JsonSerializer.Serialize(customResponse);
                    await context.Response.WriteAsync(responseJson);
                }
                if (httpMethod == HttpMethods.Put)
                {
                    context.Response.ContentType = ContentType.JSON;
                    var customResponse = new MethodNotAllowedResponse("The PUT method is not allowed for this resource. Allowed HTTP methods are GET, POST, and DELETE.");

                    var responseJson = JsonSerializer.Serialize(customResponse);
                    await context.Response.WriteAsync(responseJson);
                }
            }
        }
    }
}
