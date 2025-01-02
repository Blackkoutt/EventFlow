using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Response;
using Serilog;
using System.Security.Claims;
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
                // Pobierz role użytkownika z kontekstu
                var userRoles = context.User?.Claims
                    .Where(claim => claim.Type == "userRoles")
                    .Select(claim => claim.Value)
                    .ToList();

                // Zapisz role użytkownika w logu
                if (userRoles != null && userRoles.Any())
                {
                    Log.Information("User roles: {Roles}", string.Join(", ", userRoles));
                }
                else
                {
                    Log.Information("User has no roles or is not authenticated.");
                }

                context.Response.ContentType = ContentType.JSON;
                var customResponse = new ForbiddenResponse("Can not acces resource because of insufficient permissions.");

                var responseJson = JsonSerializer.Serialize(customResponse);
                await context.Response.WriteAsync(responseJson);
            }
        }
    }
}
